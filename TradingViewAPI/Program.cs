using TradingViewAPI.Extensions;
using TradingViewAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var host = builder.Host;
// Add services to the container.

services.ConfigureCors();
services.ConfigureMongoDBConnection(configuration);
services.ConfigureRepositories();
services.ConfigureServices(configuration);
services.ConfigureJobs();

services.AddControllers(x =>
{
    x.AllowEmptyInputInBodyModelBinding = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.StartJobs();

app.Run();
