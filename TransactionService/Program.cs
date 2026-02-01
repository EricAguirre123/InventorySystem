using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TransactionService.Application.Interfaces;
using TransactionService.Application.Services;
using TransactionService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<TransactionDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Controllers y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//clases y metodos para habilitar cors
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

// HttpClient hacia ProductService
builder.Services.AddHttpClient<IProductClient, ProductClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7102");
});

// Lógica de negocio (SIN INTERFAZ)
builder.Services.AddScoped<TransactionService.Application.Services.TransactionService>();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//agregado
app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();
app.Run();
