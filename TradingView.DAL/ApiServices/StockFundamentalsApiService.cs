using Microsoft.Extensions.Configuration;
using TradingView.DAL.Contracts;
using TradingView.DAL.Contracts.ApiServices;
using TradingView.DAL.Contracts.StockFundamentals;
using TradingView.DAL.Entities.StockFundamentals;
using TradingView.Models.Exceptions;

namespace TradingView.DAL.ApiServices;
public class StockFundamentalsApiService : IStockFundamentalsApiService
{
    private readonly IBalanceSheetRepository _balanceSheetRepository;
    private readonly ICashFlowRepository _cashFlowRepository;
    private readonly IEarningsRepository _earningsRepository;
    private readonly IFinancialsRepository _financialsRepository;
    private readonly IReportedFinancialsRepository _reportedFinancialsRepository;
    private readonly IIncomeStatementRepository _incomeStatementRepository;
    private readonly ISplitRepository _splitRepository;
    private readonly IExpirationRepository _expirationRepository;
    private readonly IOptionRepository _optionRepository;
    private readonly IDividendRepository _dividendRepository;
    private readonly ISymbolRepository _symbolRepository;

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public StockFundamentalsApiService(IBalanceSheetRepository balanceSheetRepository,
        ICashFlowRepository cashFlowRepository,
        IEarningsRepository esningsRepository,
        IFinancialsRepository financialsRepository,
        IReportedFinancialsRepository reportedFinancialsRepository,
        IIncomeStatementRepository incomeStatementRepository,
        ISplitRepository splitRepository,
        IExpirationRepository expirationRepository,
        IOptionRepository optionRepository,
        IDividendRepository dvidendRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        ISymbolRepository symbolRepository)
    {
        _balanceSheetRepository = balanceSheetRepository ?? throw new ArgumentNullException(nameof(balanceSheetRepository));
        _cashFlowRepository = cashFlowRepository ?? throw new ArgumentNullException(nameof(cashFlowRepository));
        _financialsRepository = financialsRepository ?? throw new ArgumentNullException(nameof(financialsRepository));
        _earningsRepository = esningsRepository ?? throw new ArgumentNullException(nameof(esningsRepository));
        _reportedFinancialsRepository = reportedFinancialsRepository ?? throw new ArgumentNullException(nameof(reportedFinancialsRepository));
        _incomeStatementRepository = incomeStatementRepository ?? throw new ArgumentNullException(nameof(incomeStatementRepository));
        _splitRepository = splitRepository ?? throw new ArgumentNullException(nameof(splitRepository));
        _expirationRepository = expirationRepository ?? throw new ArgumentNullException(nameof(expirationRepository));
        _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
        _dividendRepository = dvidendRepository ?? throw new ArgumentNullException(nameof(dvidendRepository));
        _symbolRepository = symbolRepository ?? throw new ArgumentNullException(nameof(symbolRepository));

        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task GetBalanceSheetApiAsync(CancellationToken ct = default)
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:balanceSheetUrl"], symbol.Symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<BalanceSheetEntity>();
            await _balanceSheetRepository.DeleteAsync(x => x.Symbol == symbol.Symbol, ct);
            await _balanceSheetRepository.AddAsync(res);
        }
    }

    public async Task GetCashFlowApiAsync(CancellationToken ct = default)
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:cashFlowUrl"], symbol.Symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<CashFlowEntity>();
            await _cashFlowRepository.DeleteAsync(x => x.Symbol == symbol.Symbol, ct);
            await _cashFlowRepository.AddAsync(res);
        }
    }

    public async Task GeDividendtApiAsync(string range, CancellationToken ct = default)
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:dividendUrl"], symbol.Symbol, range)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<List<Dividend>>();
            await _dividendRepository.DeleteAsync(x => x.Symbol == symbol.Symbol, ct); //-------------------
            await _dividendRepository.AddCollectionAsync(res);
        }
    }

    public async Task GetEarningsApiAsync(CancellationToken ct = default)
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:earningsUrl"], symbol.Symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<EarningsEntity>();
            await _earningsRepository.DeleteAsync(x => x.Symbol == symbol.Symbol, ct);
            await _earningsRepository.AddAsync(res);
        }
    }

    public async Task GetEarningsApiAsync(int last, CancellationToken ct = default)//------------------------
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:earningsRangeUrl"], symbol.Symbol, last)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<EarningsEntity>();
            // await _earningsRepository.DeleteAsync(x => x.Symbol == symbol.Symbol, ct); //---------------------
            await _earningsRepository.AddAsync(res);
        }
    }

    public async Task GetFinancialsApiAsync(CancellationToken ct = default)
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:financialsUrl"], symbol.Symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<FinancialsEntity>();
            await _financialsRepository.DeleteAsync(x => x.Symbol == symbol.Symbol, ct);
            await _financialsRepository.AddAsync(res);
        };
    }

    public async Task GetIncomeStatementApiAsync(CancellationToken ct = default)
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:incomeStatementUrl"], symbol.Symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<IncomeStatement>();
            await _incomeStatementRepository.DeleteAsync(x => x.Symbol == symbol.Symbol, ct);
            await _incomeStatementRepository.AddAsync(res);
        }
    }

    public async Task GetOptionApiAsync(CancellationToken ct = default)
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:optionUrl"], symbol.Symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<List<string>>();
            await _optionRepository.DeleteAsync(x => x.Symbol == symbol.Symbol, ct);
            var option = new OptionEntity()
            {
                Symbol = symbol.Symbol,
                Options = res
            };
            await _optionRepository.AddAsync(option);
        }
    }

    public async Task/*<List<Expiration>>*/ GetExpirationApiAsync(string expiration, CancellationToken ct = default)//---------------------------
    {
        string symbol = "AAPL";
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:optionExpirationUrl"], symbol, expiration)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<List<Expiration>>();

        await _expirationRepository.AddCollectionAsync(res);

        //return res;
    }

    public async Task GetReportedFinancialsApiAsync(CancellationToken ct = default)
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:reportedFinancialsUrl"], symbol.Symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<List<ReportedFinancials>>();
            await _reportedFinancialsRepository.DeleteAsync(x => x.Key == symbol.Symbol, ct);
            await _reportedFinancialsRepository.AddCollectionAsync(res);
        }
    }

    public async Task GetSplitApiAsync(CancellationToken ct = default)
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:splitUrl"], symbol.Symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<List<SplitEntity>>();
            await _splitRepository.DeleteAsync(x => x.Key == symbol.Symbol, ct);
            await _splitRepository.AddCollectionAsync(res);
        }
    }

    public async Task GetSplitApiAsync(string range, CancellationToken ct = default)
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:splitRangeUrl"], symbol.Symbol, range)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<List<SplitEntity>>();
            await _splitRepository.DeleteAsync(x => x.Symbol == symbol.Symbol, ct);
            await _splitRepository.AddCollectionAsync(res);
        }
    }
}