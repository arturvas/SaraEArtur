using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Wedding.API.Data;
using MercadoPago.Config;
using MercadoPago.Client.Preference;
using DotNetEnv;
using MercadoPago.Client.Payment;
using Wedding.API.Core.DTOs;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

Env.Load(".env");

var token = Environment.GetEnvironmentVariable("MERCADO_PAGO_ACCESS_TOKEN");
MercadoPagoConfig.AccessToken = token;

var sharedBackUrls = new PreferenceBackUrlsRequest
{
    Success = "https://www.saraeartur.com.br/payment/success",
    Failure = "https://www.saraeartur.com.br/payment/error",
    Pending = "https://www.saraeartur.com.br/payment/pending"
};

const string notificationUrl = "https://www.saraeartur.com.br/api/webhook";

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPass = Environment.GetEnvironmentVariable("DB_PASS");

var connectionString = $"Host={dbHost};Port=5432;Database={dbName};Username={dbUser};Password={dbPass};" 
                       + "Ssl Mode=Require; Trust Server Certificate=true;";

Console.WriteLine($"DB_HOST: {dbHost}"); // Verifique os logs no Render

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseSwagger();
app.UseSwaggerUI();

// Endpoints Minimal API
app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapGet("/api/health", () => Results.Ok("Healthy"));

app.MapGet("/api/gifts", async (AppDbContext db) =>
{ 
    var gifts = await db.Gifts.ToListAsync();

    var grouped = gifts
        .GroupBy(gift => gift.Category)
        .Select(categoryGroup => new
        {
            title = categoryGroup.Key,
            items = categoryGroup.Select(gift => new {
                id = gift.Id,
                title = gift.Title,
                price = gift.Price,
                timesTaken = gift.TimesTaken,
                lastTakenAt = gift.LastTakenAt?.ToString("dd/MM/yyyy HH:mm")
            })
        });
    
    return Results.Ok(grouped);
});

app.MapGet("/api/gifts/redirect/{id:int}", async (int id, string payerName, string payerSurname, AppDbContext db) =>
{
    var gift = await db.Gifts.FindAsync(id);
    if (gift == null) return Results.NotFound();

    var request = new List<PreferenceItemRequest>
    {
        new PreferenceItemRequest
        {
            Id = gift.Id.ToString(),
            Title = gift.Title,
            Description = "Presente da lista de casamento",
            Quantity = 1,
            CurrencyId = "BRL",
            UnitPrice = gift.Price,
            CategoryId = "others" // Categoria "Outros" no MP
        }
    };
    
    var client = new PreferenceClient();
    
    var payer = new PreferencePayerRequest
    {
        Name = payerName,
        Surname = payerSurname
    };
    
    var preferenceRequest = new PreferenceRequest()
    {
        Items = request,
        BackUrls = sharedBackUrls,
        NotificationUrl = notificationUrl,
        AutoReturn = "approved",
        ExternalReference = gift.Id.ToString(),
        Payer = payer   
    };
    
    var preference = await client.CreateAsync(preferenceRequest);
    
    // vai fazer o redirect de imediato para o link do MP
    return Results.Redirect(preference.InitPoint);
});

app.MapGet("/api/gifts/redirect/custom", async (decimal amount, string payerName, string payerSurname) =>
{
    // if (amount < 10)
    //     return Results.BadRequest("Valor mínimo de R$10,00");

    var request = new List<PreferenceItemRequest>
    {
        new PreferenceItemRequest
        {
            Id = "custom",
            Title = "Presente personalizado",
            Description = "Valor personalizado escolhido pelo convidado",
            Quantity = 1,
            CurrencyId = "BRL",
            UnitPrice = amount,
            CategoryId = "others"
        }
    };
    
    var client = new PreferenceClient();

    var payer = new PreferencePayerRequest
    {
        Name = payerName,
        Surname = payerSurname
    };

    var preferenceRequest = new PreferenceRequest()
    {
        Items = request,
        BackUrls = sharedBackUrls,
        NotificationUrl = notificationUrl,
        AutoReturn = "approved",
        ExternalReference = "custom",
        Payer = payer
    };
    
    var preference = await client.CreateAsync(preferenceRequest);
    
    // redirect imediato
    return Results.Redirect(preference.InitPoint);   
});

// app.MapPost("/api/checkout/{id:int}", async (int id, AppDbContext db) =>
// {
//     var gift = await db.Gifts.FindAsync(id);
//     if (gift == null) return Results.NotFound();
//
//     var request = new List<PreferenceItemRequest>
//     {
//         new PreferenceItemRequest
//         {
//             Id = gift.Id.ToString(),
//             Title = gift.Title,
//             Description = "Presente da lista de casamento",
//             Quantity = 1,
//             CurrencyId = "BRL",
//             UnitPrice = gift.Price,
//             CategoryId = "others" // Categoria "Outros" no Mercado Pago
//         }
//     };
//
//     var client = new PreferenceClient();
//     var preferenceRequest = new PreferenceRequest()
//     {
//         Items = request,
//         BackUrls = sharedBackUrls,
//         NotificationUrl = notificationUrl,
//         AutoReturn = "approved",
//         ExternalReference = gift.Id.ToString()
//     };
//     
//     var preference = await client.CreateAsync(preferenceRequest);
//
//     return Results.Ok(new { url = preference.InitPoint });
// });
//
// app.MapPost("/api/custom-gift", async (CustomGiftDto body) =>
// {
//     if (body.Amount < 10)
//         return Results.BadRequest("Valor mínimo de R$10,00");
//
//     var request = new List<PreferenceItemRequest>
//     {
//         new PreferenceItemRequest
//         {
//             Id = "custom",
//             Title = "Presente personalizado",
//             Description = "Valor personalizado escolhido pelo convidado",
//             Quantity = 1,
//             CurrencyId = "BRL",
//             UnitPrice = body.Amount,
//             CategoryId = "others" // Categoria "Outros" no Mercado Pago
//         }
//     };
//     
//     var client = new PreferenceClient();
//
//     var payer = new PreferencePayerRequest
//     {
//         Name = body.PayerName,
//         Surname = body.PayerSurname
//     };
//     
//     var preferenceRequest = new PreferenceRequest()
//     {
//         Items = request,
//         BackUrls = sharedBackUrls,
//         NotificationUrl = notificationUrl,
//         AutoReturn = "approved",
//         ExternalReference = "custom",
//         Payer = payer
//     };
//     
//     var preference = await client.CreateAsync(preferenceRequest);
//     
//     return Results.Ok(new { url = preference.InitPoint });   
// });

app.MapPost("/api/webhook", async (HttpRequest req, AppDbContext db) =>
{
    try
    {
        using var reader = new StreamReader(req.Body);
        var body = await reader.ReadToEndAsync();

        Console.WriteLine("Webhook recebido:");
        Console.WriteLine(body);

        if (string.IsNullOrWhiteSpace(body))
            return Results.BadRequest("Corpo vazio");

        var json = JsonSerializer.Deserialize<WebhookPayloadDto>(body, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
        Console.WriteLine($"json.Type: {json?.Type}, json.Data?.Id: {json?.Data?.Id}");
        
        if (json?.Type != "payment" || json.Data?.Id == null)
            return Results.Ok("Ignorado");

        var client = new PaymentClient();

        var payment = await client.GetAsync(json.Data.Id);
        if (payment.Status != "approved")
            return Results.Ok("Pagamento não aprovado");
    
        if (payment.ExternalReference == "custom")
        {
            Console.WriteLine("Presente personalizado recebido. Ignorado.");
            return Results.Ok("Custom gift ignorado.");
        }

        if (!int.TryParse(payment.ExternalReference, out var giftId))
            return Results.BadRequest("ExternalReference inválido");

        var gift = await db.Gifts.FindAsync(giftId);
        if (gift is null) return Results.NotFound("Presente não encontrado");

        var now = DateTime.UtcNow;

        if (gift.LastTakenAt != null && (now - gift.LastTakenAt.Value).TotalSeconds < 10)
        {
            Console.WriteLine($"Ignorado: presente {giftId} já processado recentemente.");
            return Results.Ok("Repetido ignorado.");
        }
    
        gift.TimesTaken++;
        gift.LastTakenAt = now;
        await db.SaveChangesAsync();

        Console.WriteLine($"Presente {giftId} atualizado: {gift.TimesTaken} vezes.");
        return Results.Ok("Webhook processado com sucesso");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Erro no webhook: {e.Message}");
        return Results.StatusCode(500);  
    }
});

app.MapPost("/api/admin/seed", async (HttpContext context, AppDbContext db) =>
{
    try
    {
        if (!IsAuthorized(context))
            return Results.Unauthorized();
        Console.WriteLine("Autorizado. Iniciando seed...");
        
        Console.WriteLine("Verificando se há dados no banco...");
        var hasGifts = await db.Gifts.AnyAsync();
        Console.WriteLine($"Banco respondeu: {hasGifts}");
        
        if (hasGifts)
            return Results.BadRequest("O Banco já possui dados.");
        
        GiftSeeder.Seed(db);
        Console.WriteLine("Seed aplicado com sucesso.");
        return Results.Ok("Seed aplicado com sucesso.");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Erro no endpoint /api/admin/seed: {e.Message}");
        return Results.StatusCode(500);   
    }
});

app.Run();

// Verifica a chave da API
bool IsAuthorized(HttpContext context)
{
    var expectedKey = Environment.GetEnvironmentVariable("ADMIN_API_KEY");
    if (string.IsNullOrWhiteSpace(expectedKey)) return false;

    if (!context.Request.Headers.TryGetValue("x-api-key", out var providedKey)) return false;
    
    return expectedKey == providedKey;
}
