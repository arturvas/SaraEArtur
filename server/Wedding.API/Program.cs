using Microsoft.EntityFrameworkCore;
using Wedding.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços à injeção de dependência
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middlewares
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

app.Run();
