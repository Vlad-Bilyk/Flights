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

var app = builder.Build();

app.UseCors(builder => builder.WithOrigins("*"));
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
