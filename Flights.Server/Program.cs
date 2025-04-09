using Flights.Server.Data;
using Flights.Server.Domain.Entities;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddServer(new OpenApiServer
    {
        Description = "Development Server",
        Url = "https://localhost:7066"
    });

    c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]
         + e.ActionDescriptor.RouteValues["controller"]}");
});

builder.Services.AddSingleton<Entities>();

var app = builder.Build();

var entities = app.Services.CreateScope().ServiceProvider.GetService<Entities>();
var random = new Random();

Flight[] flightsToSeed =
[
    new(Guid.NewGuid(),
        "American Airlines",
        random.Next(90, 5000).ToString(),
        new TimePlace("Los Angeles", DateTime.Now.AddHours(random.Next(1, 3))),
        new TimePlace("Istanbul", DateTime.Now.AddHours(random.Next(4, 10))),
        random.Next(1, 853)),
    new(Guid.NewGuid(),
        "Deutsche BA",
        random.Next(90, 5000).ToString(),
        new TimePlace("Munchen", DateTime.Now.AddHours(random.Next(1, 10))),
        new TimePlace("Schiphol", DateTime.Now.AddHours(random.Next(4, 15))),
        random.Next(1, 853)),
    new(Guid.NewGuid(),
        "British Airways",
        random.Next(90, 5000).ToString(),
        new TimePlace("London, England", DateTime.Now.AddHours(random.Next(1, 15))),
        new TimePlace("Vizzola-Ticino", DateTime.Now.AddHours(random.Next(4, 18))),
        random.Next(1, 853)),
    new(Guid.NewGuid(),
    "BB Heliag",
        random.Next(90, 5000).ToString(),
        new TimePlace("Zurich", DateTime.Now.AddHours(random.Next(1, 15))),
        new TimePlace("Baku", DateTime.Now.AddHours(random.Next(4, 19))),
        random.Next(1, 853)),
    new(Guid.NewGuid(),
        "Adria Airways",
        random.Next(90, 5000).ToString(),
        new TimePlace("Ljubljana", DateTime.Now.AddHours(random.Next(1, 15))),
        new TimePlace("Warshaw", DateTime.Now.AddHours(random.Next(4, 19))),
        random.Next(1, 853)),
    new(Guid.NewGuid(),
        "ABA Air",
        random.Next(90, 5000).ToString(),
        new TimePlace("Praha Ruzyne", DateTime.Now.AddHours(random.Next(1, 55))),
        new TimePlace("Paris", DateTime.Now.AddHours(random.Next(4, 58))),
        random.Next(1, 853)),
];
entities.Flights.AddRange(flightsToSeed);

app.UseCors(builder => builder
.WithOrigins("*")
.AllowAnyMethod()
.AllowAnyHeader());

app.UseSwagger().UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
