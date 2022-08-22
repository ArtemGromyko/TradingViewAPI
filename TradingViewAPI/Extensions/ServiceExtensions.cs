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
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IFundamentalsRepository, FundamentalsRepository>();
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
    }

    public static void ConfigureSections(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BookStoreDatabaseSettings>(configuration.GetSection("BookStoreDatabase"));
        services.Configure<FundamentalsStoreDatabaseSettings>(configuration
            .GetSection("FundamentalseStoreDatabse"));
    }
}
