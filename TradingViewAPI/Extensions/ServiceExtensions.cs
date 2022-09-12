using TradingView.BLL.Contracts.RealTime;
using TradingView.BLL.Contracts.StockFundamentals;
using TradingView.BLL.Contracts.StockProfile;
using TradingView.BLL.Services.RealTime;
using TradingView.BLL.Services.StockFundamentals;
using TradingView.BLL.Services.StockProfile;
using TradingView.DAL.Contracts;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Contracts.StockFundamentals;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Repositories.RealTime;
using TradingView.DAL.Repositories.StockFundamentals;
using TradingView.DAL.Repositories.StockProfile;
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
        services.AddScoped<ILargestTradesRepository, LargestTradesRepository>();
        services.AddScoped<IOHLCRepository, OHLCRepository>();
        services.AddScoped<IPreviousDayPriceRepository, PreviousDayPriceRepository>();
        services.AddScoped<IVolumeByVenueRepository, VolumeByVenueRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IDelayedQuoteRepository, DelayedQuoteRepository>();
        services.AddScoped<IPriceOnlyRepository, PriceOnlyRepository>();

        services.AddScoped<ILogoRepository, LogoRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICEOCompensationRepository, CEOCompensationRepository>();
        services.AddScoped<IInsiderRosterRepository, InsiderRosterRepository>();
        services.AddScoped<IInsiderSummaryRepository, InsiderSummaryRepository>();
        services.AddScoped<IInsiderTransactionsRepository, InsiderTransactionsRepository>();
        services.AddScoped<IPeerGroupRepository, PeerGroupRepository>();

        services.AddScoped<IBalanceSheetRepository, BalanceSheetRepository>();
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
        services.AddScoped<ILargestTradesService, LargestTradesService>();
        services.AddScoped<IOHLCService, OHLCService>();
        services.AddScoped<IPreviousDayPriceService, PreviousDayPriceService>();
        services.AddScoped<IPriceOnlyService, PriceOnlyService>();
        services.AddScoped<IVolumeByVenueService, VolumeByVenueService>();
        services.AddScoped<ILogoService, LogoService>();
        services.AddScoped<ICEOCompensationService, CEOCompensationService>();
        services.AddScoped<IСompanyService, СompanyService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IDelayedQuoteService, DelayedQuoteService>();

        services.AddScoped<IInsiderRosterService, InsiderRosterService>();
        services.AddScoped<IInsiderSummaryService, InsiderSummaryService>();
        services.AddScoped<IInsiderTransactionsService, InsiderTransactionsService>();
        services.AddScoped<IPeerGroupService, PeerGroupService>();

        services.AddScoped<IBalanceSheetService, BalanceSheetService>();
    }

    public static void ConfigureMongoDBConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration
            .GetSection("DatabaseSettings"));
    }
}
