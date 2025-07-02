using Microsoft.EntityFrameworkCore;
using Wedding.API.Data;
using MercadoPago.Config;
using MercadoPago.Client.Preference;
using DotNetEnv;

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
app.MapGet("/gifts", async (AppDbContext db) =>
{
    var gifts = await db.Gifts
        .Select(gift => new { gift.Id, gift.Name, gift.Price, gift.Taken })
        .ToListAsync();

    return Results.Ok(gifts);
});

app.MapPost("/checkout/{id}", async (int id, AppDbContext db) =>
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
        Items = request
    };
    
    var preference = await client.CreateAsync(preferenceRequest);

    return Results.Ok(new { url = preference.InitPoint });
});

app.Run();
