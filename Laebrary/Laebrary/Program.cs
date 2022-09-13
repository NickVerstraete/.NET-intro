using Laebrary.Models;
using Laebrary.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LaebraryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LaebraryDb")));
       
//Add Repositories
builder.Services.AddTransient<IBookRepository, BookRepository>();

//Add Services

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


// Make the implicit Program class public so test projects can access it
public partial class Program { }