using Backend.Data;
using Backend.Validators;
using Microsoft.EntityFrameworkCore;
using Backend.Services;
using Backend.Repositories;
using FluentValidation;
using Backend.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>();

// Configuração da conexão com o banco de dados
var connectionString = builder.Configuration.GetConnectionString("ConnectionPadrao");
builder.Services.AddDbContext<PetDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configuração do OpenAPI (Swagger)
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<ExceptionHandlingMiddleware>();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
