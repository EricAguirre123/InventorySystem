using Microsoft.EntityFrameworkCore;
using ProductService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS ✅
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// 🔴 CORS DEBE IR AQUÍ (ANTES de Authorization)
app.UseCors("AllowAngular");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();
