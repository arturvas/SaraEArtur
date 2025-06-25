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
app.MapGet("/", () => "Hello World!");
app.MapGet("/gifts", async (AppDbContext db) => await db.Gifts.ToListAsync());

app.Run();
