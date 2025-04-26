using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PruebaTecnica.Shared.Domain.Repositories;
using PruebaTecnica.Shared.Persistence.Repositories;
using PruebaTecnica.Pedidos.Domain.Repositories;
using PruebaTecnica.Pedidos.Persistence.Repositories;
using PruebaTecnica.Pedidos.Services;
using PruebaTecnica.Pedidos.Domain.Services;
using PruebaTecnica.Shared.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors();

// Add controllers
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Prueba Técnica API",
        Description = "RESTful API para gestión de Pedidos",
        Contact = new OpenApiContact
        {
            Name = "Luis Pineda Ugas",
            Url = new Uri("https://github.com/LuisPineda05")
        }
    });
});

// Database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(connectionString)
           .LogTo(Console.WriteLine, LogLevel.Information)
           .EnableSensitiveDataLogging()
           .EnableDetailedErrors());

// Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// AutoMapper
builder.Services.AddAutoMapper(
    typeof(PruebaTecnica.Pedidos.Mapping.ModelToResourceProfile),
    typeof(PruebaTecnica.Pedidos.Mapping.ResourceToModelProfile));

// Dependency Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();

builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

builder.Services.AddScoped<IProductoPedidoRepository, PedidoProductoRepository>();
builder.Services.AddScoped<IPedidoProductoService, PedidoProductoService>();

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });

    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
