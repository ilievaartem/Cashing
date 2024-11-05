using MySql.Data.MySqlClient;
using System.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Налаштування для контролерів
builder.Services.AddControllers();

// Налаштування Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "DogMateSocialMedia API",
        Description = "An ASP.NET Core Web API for managing users and likes",
    });
});

// Налаштування конфігурації (appsettings.json)
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Підключення MySQL через MySqlConnection
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connection = new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
    connection.Open();
    return connection;
});

// Підключення транзакцій MySQL
builder.Services.AddScoped<IDbTransaction>(sp =>
{
    var connection = sp.GetRequiredService<MySqlConnection>();
    connection.Open();
    return connection.BeginTransaction();
});

// Додаємо In-Memory Cache
builder.Services.AddMemoryCache();

// Додаємо Distributed Cache з Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

// Реєстрація репозиторіїв
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();

// Реєстрація сервісів
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILikeService, LikeService>();

var app = builder.Build();

// Налаштування середовища розробки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS та авторизація
app.UseHttpsRedirection();
app.UseAuthorization();

// Маршрутизація контролерів
app.MapControllers();

app.Run();
