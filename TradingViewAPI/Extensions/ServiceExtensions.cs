using BookStoreApi.Models;
using TradingView.BLL.Contracts;
using TradingView.BLL.Services;
using TradingView.DAL.Contracts;
using TradingView.DAL.Repositories;
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
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IHistoricalPricesService, HistoricalPricesService>();
    }

    public static void ConfigureSections(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BookStoreDatabaseSettings>(configuration.GetSection("BookStoreDatabase"));
        services.Configure<FundamentalsStoreDatabaseSettings>(configuration
            .GetSection("FundamentalseStoreDatabse"));
    }
}
