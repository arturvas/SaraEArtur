using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Wedding.API.Data;
using MercadoPago.Config;
using MercadoPago.Client.Preference;
using DotNetEnv;
using MercadoPago.Client.Payment;
using Wedding.API.Core.DTOs;

var builder = WebApplication.CreateBuilder(args);

Env.Load(".env");

var token = Environment.GetEnvironmentVariable("MERCADO_PAGO_ACCESS_TOKEN");
MercadoPagoConfig.AccessToken = token;

var sharedBackUrls = new PreferenceBackUrlsRequest
{
    Success = "https://www.saraeartur.com.br/payment/success",
    Failure = "https://www.saraeartur.com.br/payment/error",
    Pending = "https://www.saraeartur.com.br/payment/pending"
};

var notificationUrl = "https://www.saraeartur.com.br/api/webhook";

// Adiciona serviços à injeção de dependência
var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                       $"Port=5432;" +
                       $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                       $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                       $"Password={Environment.GetEnvironmentVariable("DB_PASS")};" +
                       $"Ssl Mode=Disable";

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseSwagger();
app.UseSwaggerUI();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

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


app.MapPost("/api/checkout/{id}", async (int id, AppDbContext db) =>
{
    var gift = await db.Gifts.FindAsync(id);
    if (gift == null) return Results.NotFound();

    var request = new List<PreferenceItemRequest>
    {
        new PreferenceItemRequest
        {
            Title = gift.Title,
            Quantity = 1,
            CurrencyId = "BRL",
            UnitPrice = gift.Price
        }
    };

    var client = new PreferenceClient();
    var preferenceRequest = new PreferenceRequest()
    {
        Items = request,
        BackUrls = sharedBackUrls,
        NotificationUrl = notificationUrl,
        AutoReturn = "approved",
        ExternalReference = gift.Id.ToString()
    };
    
    var preference = await client.CreateAsync(preferenceRequest);

    return Results.Ok(new { url = preference.InitPoint });
});

app.MapPost("/api/custom-gift", async (CustomGiftDto body) =>
{
    if (body.Amount < 10)
        return Results.BadRequest("Valor mínimo de R$10,00");

    var request = new List<PreferenceItemRequest>
    {
        new PreferenceItemRequest
        {
            Title = "Presente personalizado",
            Quantity = 1,
            CurrencyId = "BRL",
            UnitPrice = body.Amount
        }
    };
    
    var client = new PreferenceClient();
    var preferenceRequest = new PreferenceRequest()
    {
        Items = request,
        BackUrls = sharedBackUrls,
        NotificationUrl = notificationUrl,
        AutoReturn = "approved",
        ExternalReference = "custom"
    };
    
    var preference = await client.CreateAsync(preferenceRequest);
    
    return Results.Ok(new { url = preference.InitPoint });   
});

app.MapPost("/api/webhook", async (HttpRequest req, AppDbContext db) =>
{
    using var reader = new StreamReader(req.Body);
    var body = await reader.ReadToEndAsync();

    Console.WriteLine("Webhook recebido:");
    Console.WriteLine(body);

    if (string.IsNullOrWhiteSpace(body))
        return Results.BadRequest("Corpo vazio");

    var json = JsonSerializer.Deserialize<WebhookPayloadDto>(body);
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

    if (gift.LastTakenAt != null && (now - gift.LastTakenAt.Value).TotalSeconds < 30)
    {
        Console.WriteLine($"Ignorado: presente {giftId} já processado recentemente.");
        return Results.Ok("Repetido ignorado.");
    }
    
    gift.TimesTaken++;
    gift.LastTakenAt = now;
    await db.SaveChangesAsync();

    Console.WriteLine($"Presente {giftId} atualizado: {gift.TimesTaken} vezes.");
    return Results.Ok("Webhook processado com sucesso");
});

// Seeder
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    db.Database.Migrate();
    GiftSeeder.Seed(db);
}

app.Run();
