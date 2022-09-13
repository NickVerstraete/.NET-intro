using Laebrary.Models;
using Laebrary.Repositories;
using Laebrary.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<LaebraryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerLaebraryDb")));
builder.Services.AddDbContext<LaebraryContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("MySqlLaebraryDb"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlLaebraryDb"))));

//Add Repositories
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IMemberRepository, MemberRepository>();
builder.Services.AddTransient<ILendingRepository, LendingRepository>();
 

//Add Services
builder.Services.AddTransient<ILendingService, LendingService>();

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