using TradingViewAPI.Extensions;
using TradingViewAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var host = builder.Host;
// Add services to the container.

services.ConfigureMongoDBConnection(configuration);
services.ConfigureRepositories();
services.ConfigureServices(configuration);
services.ConfigureJobs();

services.AddControllers(x =>
{
    x.AllowEmptyInputInBodyModelBinding = true;
    x.Filters.Add(typeof(ExceptionHandlingFilter));
});

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
app.StartJobs();

app.Run();
