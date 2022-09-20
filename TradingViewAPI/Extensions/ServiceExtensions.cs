using TradingView.BLL.Contracts;
using TradingView.BLL.Contracts.RealTime;
using TradingView.BLL.Contracts.StockFundamentals;
using TradingView.BLL.Contracts.StockProfile;
using TradingView.BLL.Services;
using TradingView.BLL.Services.RealTime;
using TradingView.BLL.Services.StockFundamentals;
using TradingView.BLL.Services.StockProfile;
using TradingView.DAL.ApiServices;
using TradingView.DAL.Contracts;
using TradingView.DAL.Contracts.ApiServices;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Contracts.StockFundamentals;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Jobs;
using TradingView.DAL.Jobs.Jobs;
using TradingView.DAL.Jobs.Jobs.StockFundamentals;
using TradingView.DAL.Jobs.Jobs.StockProfile;
using TradingView.DAL.Jobs.Schedulers;
using TradingView.DAL.Jobs.Schedulers.StockFundamentals;
using TradingView.DAL.Jobs.Schedulers.StockProfile;
using TradingView.DAL.Repositories;
using TradingView.DAL.Repositories.RealTime;
using TradingView.DAL.Repositories.StockFundamentals;
using TradingView.DAL.Repositories.StockProfile;
using TradingView.DAL.Settings;

namespace TradingViewAPI.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

    public static void ConfigureJobs(this IServiceCollection services)
    {
        services.AddTransient<JobFactory>();
        services.AddScoped<SymbolJob>();

        services.AddScoped<CEOCompensationJob>();
        services.AddScoped<PeerGroupJob>();
        services.AddScoped<LogoJob>();
        services.AddScoped<InsiderSummaryJob>();
        services.AddScoped<InsiderTransactionsJob>();
        services.AddScoped<InsiderRosterJob>();
        services.AddScoped<CompanyJob>();


        services.AddScoped<ReportedFinancialsJob>();
        services.AddScoped<IncomeStatementJob>();
        services.AddScoped<OptionJob>();
        services.AddScoped<FinancialsJob>();
        services.AddScoped<ExpirationJob>();
        services.AddScoped<CashFlowJob>();
        services.AddScoped<DividendJob>();
        services.AddScoped<EarningsJob>();
        services.AddScoped<SplitJob>();

    }

    public static void StartJobs(this WebApplication host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            try
            {
                //SymbolScheduler.Start(serviceProvider);
                //CEOCompensationScheduler.Start(serviceProvider);
                //PeerGroupScheduler.Start(serviceProvider);
                //LogoScheduler.Start(serviceProvider);
                //InsiderSummaryScheduler.Start(serviceProvider);
                //InsiderTransactionsScheduler.Start(serviceProvider);
                //InsiderRosterScheduler.Start(serviceProvider);
                CompanyScheduler.Start(serviceProvider);

                //ReportedFinancialsScheduler.Start(serviceProvider);
                //IncomeStatementScheduler.Start(serviceProvider);
                //OptionScheduler.Start(serviceProvider);
                //FinancialsScheduler.Start(serviceProvider);
                //ExpirationScheduler.Start(serviceProvider);
                //CashFlowScheduler.Start(serviceProvider);
                //DividendScheduler.Start(serviceProvider);
                //EarningsScheduler.Start(serviceProvider);
                //SplitScheduler.Start(serviceProvider);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public static void ConfigureRepositories(this IServiceCollection services)
    {
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
        services.AddScoped<ICashFlowRepository, CashFlowRepository>();
        services.AddScoped<IEarningsRepository, EarningsRepository>();
        services.AddScoped<IFinancialsRepository, FinancialsRepository>();
        services.AddScoped<IReportedFinancialsRepository, ReportedFinancialsRepository>();
        services.AddScoped<IIncomeStatementRepository, IncomeStatementRepository>();
        services.AddScoped<ISplitRepository, SplitRepository>();
        services.AddScoped<IExpirationRepository, ExpirationRepository>();
        services.AddScoped<IOptionRepository, OptionRepository>();
        services.AddScoped<IDividendRepository, DividendRepository>();

        services.AddScoped<ISymbolRepository, SymbolRepository>();
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
        services.AddScoped<ICashFlowService, CashFlowService>();
        services.AddScoped<IFinancialsService, FinancialsService>();
        services.AddScoped<IReportedFinancialsService, ReportedFinancialsService>();
        services.AddScoped<IIncomeStatementService, IncomeStatementService>();
        services.AddScoped<ISplitService, SplitService>();
        services.AddScoped<IOptionService, OptionService>();
        services.AddScoped<IEarningsService, EarningsService>();
        services.AddScoped<IDividendService, DividendService>();

        services.AddScoped<IStockFundamentalsApiService, StockFundamentalsApiService>();
        services.AddScoped<IStockProfileApiService, StockProfileApiService>();

        services.AddScoped<ISymbolService, SymbolService>();
    }

    public static void ConfigureMongoDBConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration
            .GetSection("DatabaseSettings"));
    }
}
