using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Wedding.API.Data;
using MercadoPago.Config;
using MercadoPago.Client.Preference;
using DotNetEnv;
using MercadoPago.Client.Payment;
using Wedding.API.Core.DTOs;
using Wedding.API.Core.Models;

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

const string notificationUrl = "https://saraeartur-wedding-api.onrender.com/api/webhook";

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPass = Environment.GetEnvironmentVariable("DB_PASS");

var connectionString = $"Host={dbHost};Port=5432;Database={dbName};Username={dbUser};Password={dbPass};"
                       + "Ssl Mode=Require; Trust Server Certificate=true;Timeout=300;CommandTimeout=300;";

Console.WriteLine($"DB_HOST: {dbHost}");

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
            items = categoryGroup.Select(gift => new
            {
                id = gift.Id,
                title = gift.Title,
                price = gift.Price,
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
        Payer = payer,
        PaymentMethods = GetPaymentMethodsWithoutPixAndBoleto(),
        Metadata = new Dictionary<string, object>
        {
            ["payerName"] = payerName,
            ["payerSurname"] = payerSurname,
        }
    };

    var preference = await client.CreateAsync(preferenceRequest);

    // vai fazer o redirect de imediato para o link do MP
    return Results.Redirect(preference.InitPoint);
});

app.MapGet("/api/gifts/redirect/custom", async (decimal amount, string payerName, string payerSurname) =>
{
    if (amount < 10)
        return Results.BadRequest("Valor mínimo de R$10,00");

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
        Payer = payer,
        PaymentMethods = GetPaymentMethodsWithoutPixAndBoleto(),
        Metadata = new Dictionary<string, object>
        {
            ["payerName"] = payerName,
            ["payerSurname"] = payerSurname
        }
    };

    var preference = await client.CreateAsync(preferenceRequest);

    // redirect imediato
    return Results.Redirect(preference.InitPoint);
});

app.MapPost("/api/webhook", async (HttpRequest req, AppDbContext db) =>
{
    Console.WriteLine("Entrou no webhook!");

    try
    {
        using var reader = new StreamReader(req.Body);
        var body = await reader.ReadToEndAsync();

        Console.WriteLine("Webhook recebido:");
        Console.WriteLine(body);

        if (string.IsNullOrWhiteSpace(body))
        {
            Console.WriteLine("Corpo vazio");
            return Results.BadRequest("Corpo vazio");
        }

        WebhookPayloadDto? json = null;
        try
        {
            json = JsonSerializer.Deserialize<WebhookPayloadDto>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }
        catch (Exception e)
        {
            Console.WriteLine($"Falha ao deserializar webhook: {e.Message}");
            Console.WriteLine("Corpo recebido:");
            Console.WriteLine(body);
            return Results.BadRequest("Erro de serialização");
        }

        if (json?.Type != "payment" || json.Data?.Id == null)
            return Results.Ok("Ignorado");

        var client = new PaymentClient();

        var payment = await client.GetAsync(long.Parse(json.Data.Id));

        if (payment.Status != "approved")
            return Results.Ok("Pagamento não aprovado");

        Console.WriteLine($"Tipo: {json?.Type}, ExternalReference: {payment.ExternalReference}");

        if (payment.ExternalReference == "custom")
        {
            Console.WriteLine("Presente personalizado recebido. Ignorado.");
            return Results.Ok("Custom gift ignorado.");
        }

        if (!int.TryParse(payment.ExternalReference, out var giftId))
            return Results.BadRequest("ExternalReference inválido");

        var gift = await db.Gifts.FindAsync(giftId);
        if (gift is null) return Results.NotFound("Presente não encontrado");

        var payerName = payment.Metadata != null && payment.Metadata.TryGetValue("payerName", out var metaFist) 
            ? metaFist?.ToString()
            : payment.Payer.FirstName;
        
        var payerSurname = payment.Metadata != null && payment.Metadata.TryGetValue("payerSurname", out var metaLast)
            ? metaLast?.ToString()
            : payment.Payer.LastName;

        var payerFullName = $"{payerName} {payerSurname}".Trim();
            
        db.GiftOrders.Add(new GiftOrder
        {
            GiftId = giftId,
            Title = gift.Title,
            PayerFullName = payerFullName,
            PayerFirstName = payerName,
            PayerLastName = payerSurname,
            Amount = payment.TransactionAmount ?? gift.Price,
            PaidAt = DateTime.UtcNow,
            PayerEmail = payment.Payer.Email
        });
        await db.SaveChangesAsync();

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

// Exclui os pagamentos com PIX e Boleto
PreferencePaymentMethodsRequest GetPaymentMethodsWithoutPixAndBoleto()
{
    return new PreferencePaymentMethodsRequest
    {
        ExcludedPaymentTypes = new List<PreferencePaymentTypeRequest>
        {
            new() { Id = "ticket" },
            new() { Id = "bank_transfer" }
        }
    };
}

app.Run();

// Verifica a chave da API
bool IsAuthorized(HttpContext context)
{
    var expectedKey = Environment.GetEnvironmentVariable("ADMIN_API_KEY");
    if (string.IsNullOrWhiteSpace(expectedKey)) return false;

    if (!context.Request.Headers.TryGetValue("x-api-key", out var providedKey)) return false;

    return expectedKey == providedKey;
}
