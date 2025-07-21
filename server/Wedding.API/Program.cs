using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Wedding.API.Data;
using MercadoPago.Config;
using MercadoPago.Client.Preference;
using DotNetEnv;
using Wedding.API.Core.Dtos;

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

app.MapPost("/api/custom-gift", async (HttpRequest req) =>
{
    var body = await req.ReadFromJsonAsync<CustomGiftDto>();
    if (body is null || body.Amount < 10)
        return Results.BadRequest("Valor mínimo: R$10,00");

    // TODO integrar Mercado Pago pra gerar um link
    // simulando resposta

    return Results.Ok(new { paymentUrl = "https://pagamento.mercadopago.com/custom123456789" });
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
    
    using var doc = JsonDocument.Parse(body);
    var root = doc.RootElement;

    // Verifica se é um pagamento aprovado
    if (!root.TryGetProperty("type", out var typeProp) || typeProp.GetString() != "payment") return Results.Ok();
    if (!root.TryGetProperty("data", out var dataProp) ||
        !dataProp.TryGetProperty("id", out var paymentIdProp)) return Results.Ok();
    var paymentId = paymentIdProp.GetInt64();

    // Aqui você pode consultar a API do Mercado Pago pra obter detalhes do pagamento
    var client = new MercadoPago.Client.Payment.PaymentClient();
    var payment = await client.GetAsync(paymentId);

    if (payment.Status != "approved" || payment.ExternalReference == null || !int.TryParse(payment.ExternalReference, out int giftId)) return Results.Ok();
    var gift = await db.Gifts.FindAsync(giftId);
    if (gift is null) return Results.Ok();
    gift.TimesTaken++;
    await db.SaveChangesAsync();

    Console.WriteLine($"Gift ID {giftId} atualizado: TimesTaken = {gift.TimesTaken}");

    return Results.Ok();
});

// Seeder
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    db.Database.Migrate();
    GiftSeeder.Seed(db);
}

app.Run();
