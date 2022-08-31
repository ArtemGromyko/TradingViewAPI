using TradingView.BLL.Contracts.RealTime;
using TradingView.BLL.Services.RealTime;
using TradingView.DAL.Contracts;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Repositories.RealTime;
using TradingView.DAL.Settings;

namespace TradingViewAPI.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISymbolsRepository, SymbolsRepository>();
        services.AddScoped<IDividendsRepository, DividendsRepository>();
        services.AddScoped<IExchangesRepository, ExchangesRepository>();

        services.AddScoped<IHistoricalPricesRepository, HistoricalPricesRepository>();
        services.AddScoped<IQuotesRepository, QuotesRepository>();
        services.AddScoped<IIntradayPricesRepository, IntradayPricesRepository>();
    }

    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient(configuration["HttpClientName"], client =>
        {
            client.BaseAddress = new Uri(configuration["IEXCloudUrls:baseUrl"]);
        });

        services.AddScoped<IRealTimeService, RealTimeService>();
        services.AddScoped<IHistoricalPricesService, HistoricalPricesService>();
        services.AddScoped<IQuotesService, QuotesService>();
        services.AddScoped<IIntradayPricesService, IntradayPricesService>();
    }

    public static void ConfigureMongoDBConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration
            .GetSection("DatabaseSettings"));
    }
}
