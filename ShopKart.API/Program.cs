using Microsoft.EntityFrameworkCore;
using ShopKart.API.Data;
using ShopKart.API.Middleware;
using ShopKart.API.Repositories.Implementations;
using ShopKart.API.Repositories.Interfaces;
using ShopKart.API.Services.Implementations;
using ShopKart.API.Services.Interfaces;
using ShopKart.API.Strategies;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// ===== ADD SERVICES TO CONTAINER =====

// Register DbContext with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Controllers
builder.Services.AddControllers();

// Custom Repo/Services registration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// strategy resolver dependencies
builder.Services.AddTransient<CreditCardPayment>();
builder.Services.AddTransient<UpiPayment>();
builder.Services.AddTransient<CodPayment>();
builder.Services.AddTransient<IPaymentStrategyResolver,PaymentStrategyResolver>();


// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ===== CONFIGURE HTTP REQUEST PIPELINE =====

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();