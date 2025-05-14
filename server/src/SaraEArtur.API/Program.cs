using SaraEArtur.API.Routes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.GiftsRoutes();

// app.MapPost("orders");
//
// app.MapGet("orders/{id}/status");
//
// app.MapGet("orders");
//
// app.MapPatch("gifts/{id}");

app.UseHttpsRedirection();
app.Run();
