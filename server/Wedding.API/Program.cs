using Microsoft.EntityFrameworkCore;
using Wedding.API.Data;
using MercadoPago.Config;
using MercadoPago.Client.Preference;
using DotNetEnv;
using Wedding.API.Core.Models;

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
app.UseHttpsRedirection();

// Endpoints Minimal API

app.MapGet("/api/heath", () => Results.Ok("Healthy"));

app.MapGet("/api/gifts", async (AppDbContext db) =>
{
    var gifts = await db.Gifts
        .Select(gift => new { gift.Id, gift.Name, gift.Price, gift.Taken })
        .ToListAsync();

    return Results.Ok(gifts);
});

app.MapPost("/api/checkout/{id}", async (int id, AppDbContext db) =>
{
    var gift = await db.Gifts.FindAsync(id);
    if (gift == null) return Results.NotFound();

    var request = new List<PreferenceItemRequest>
    {
        new PreferenceItemRequest
        {
            Title = gift.Name,
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
    
    Console.WriteLine("Webhook recebido: ");
    Console.WriteLine(body);
    
    // TODO: processar JSON, verificar tipo, atualizar presente...
    
    return Results.Ok();
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    db.Database.Migrate();

    if (!db.Gifts.Any())
    {
        db.Gifts.AddRange(
            new Gift { Name = "Jogo de cama", ImageUrl = "image/jg-cama", Price = 200, Taken = false},
            new Gift { Name = "Panela de pressão", ImageUrl = "image/pnl-pressao", Price = 150, Taken = false},
            new Gift { Name = "Liquidificador", ImageUrl = "image/liquidificador", Price = 220, Taken = false}
        );
        
        db.SaveChanges();
        Console.WriteLine("Presentes iniciais adicionados.");
    }
    else
    {
        Console.WriteLine("Presentes ja existem, nenhum novo foi add.");
    }
}

app.Run();
