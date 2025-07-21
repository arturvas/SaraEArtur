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

// Adiciona serviços à injeção de dependência
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
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
                timesTaken = gift.TimesTaken
            })
        });
    
    return Results.Ok(grouped);
});

app.MapPost("/api/custom-gift", (CustomGiftDto body) => 
    body.Amount < 10 ? Results.BadRequest("Valor mínimo: R$10,00") :
       
    // TODO integrar Mercado Pago pra gerar um link
    // Simula resposta
    
    Results.Ok(new { paymentUrl = "https://pagamento.mercadopago.com/custom123456789" }));

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
        BackUrls = new PreferenceBackUrlsRequest
        {
            Success = "https://www.saraeartur.com.br/sucesso",
            Failure = "https://www.saraeartur.com.br/erro",
            Pending = "https://www.saraeartur.com.br/pendente"
        },
        AutoReturn = "approved",
        ExternalReference = gift.Id.ToString()
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

    if (!int.TryParse(payment.ExternalReference, out var giftId))
        return Results.BadRequest("ExternalReference inválido");

    var gift = await db.Gifts.FindAsync(giftId);
    if (gift is null) return Results.NotFound("Presente não encontrado");

    if (gift.TimesTaken == 0)
    {
        gift.TimesTaken++;
        await db.SaveChangesAsync();
        Console.WriteLine($"Gift {giftId} atualizado: {gift.TimesTaken}x");
    }
    else
    {
        Console.WriteLine($"Webhook ignorado: Gift {giftId} já foi processado ({gift.TimesTaken}x).");
    }

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
