using Microsoft.EntityFrameworkCore;
using SaraEArtur.API.Data;
using SaraEArtur.API.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WeddingContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddScoped<WeddingContext>();
builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(p => p.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.GiftsRoutes();

// TODO app.MapPost("orders");

// TODO app.MapGet("orders/{id}/status");

// TODO app.MapGet("orders");

// TODO app.MapPatch("gifts/{id}");

app.UseHttpsRedirection();
app.Run();
