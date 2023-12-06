using Microsoft.EntityFrameworkCore;
using PruebaDesarrollo.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "PRACTICA",
    builder =>
    {
     builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TrabajadoresPruebaContext>(opt =>

{

    opt.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBTAMBO");

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PRACTICA");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

