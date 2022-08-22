using BookStoreApi.Models;
using TradingView.BLL.Contracts;
using TradingView.BLL.Services;
using TradingView.DAL.Contracts;
using TradingView.DAL.Repositories;
using TradingView.DAL.Settings;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.

services.Configure<BookStoreDatabaseSettings>(configuration.GetSection("BookStoreDatabase"));
services.Configure<FundamentalsStoreDatabaseSettings>(configuration
    .GetSection("FundamentalseStoreDatabse"));

services.AddScoped<IBookRepository, BookRepository>();
services.AddScoped<IFundamentalsRepository, FundamentalsRepository>();

services.AddScoped<IBookService, BookService>();
services.AddScoped<IFundamentalsRepository, FundamentalsRepository>();

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

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
