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

    public async Task GetBalanceSheetApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
           $"{string.Format(_configuration["IEXCloudUrls:balanceSheetUrl"], symbol)}" +
           $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<BalanceSheetEntity>();

        await _balanceSheetRepository.AddAsync(res);
    }


    public async Task GetCashFlowApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
           $"{string.Format(_configuration["IEXCloudUrls:cashFlowUrl"], symbol)}" +
           $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<CashFlowEntity>();
        await _cashFlowRepository.AddAsync(res);
    }


    public async Task GeDividendtApiAsync(string symbol, string range, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
           $"{string.Format(_configuration["IEXCloudUrls:dividendUrl"], symbol, range)}" +
           $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<List<Dividend>>();
        await _dividendRepository.AddCollectionAsync(res);
    }

    public async Task GetEarningsApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
           $"{string.Format(_configuration["IEXCloudUrls:earningsUrl"], symbol)}" +
           $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<EarningsEntity>();
        await _earningsRepository.AddAsync(res);
    }

    public async Task GetEarningsApiAsync(string symbol, int last, CancellationToken ct = default)//------------------------
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
           $"{string.Format(_configuration["IEXCloudUrls:earningsRangeUrl"], symbol, last)}" +
           $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<EarningsEntity>();
        await _earningsRepository.AddAsync(res);
    }

    public async Task GetFinancialsApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
           $"{string.Format(_configuration["IEXCloudUrls:financialsUrl"], symbol)}" +
           $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<FinancialsEntity>();

        await _financialsRepository.AddAsync(res);
    }

    public async Task GetIncomeStatementApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
           $"{string.Format(_configuration["IEXCloudUrls:incomeStatementUrl"], symbol)}" +
           $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<IncomeStatement>();
        await _incomeStatementRepository.AddAsync(res);
    }

    public async Task GetOptionApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
           $"{string.Format(_configuration["IEXCloudUrls:optionUrl"], symbol)}" +
           $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<List<string>>();
        var option = new OptionEntity()
        {
            Symbol = symbol,
            Options = res
        };
        await _optionRepository.AddAsync(option);
    }

    public async Task/*<List<Expiration>>*/ GetExpirationApiAsync(string symbol, string expiration, CancellationToken ct = default)//---------------------------
    {
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

    public async Task GetReportedFinancialsApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
           $"{string.Format(_configuration["IEXCloudUrls:reportedFinancialsUrl"], symbol)}" +
           $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<List<ReportedFinancials>>();
        await _reportedFinancialsRepository.AddCollectionAsync(res);

    }

    public async Task GetSplitApiAsync(string symbol, CancellationToken ct = default)
    {

        var url = $"{_configuration["IEXCloudUrls:version"]}" +
           $"{string.Format(_configuration["IEXCloudUrls:splitUrl"], symbol)}" +
           $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<List<SplitEntity>>();
        await _splitRepository.AddCollectionAsync(res);
    }


    public async Task GetSplitApiAsync(string symbol, string range, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
           $"{string.Format(_configuration["IEXCloudUrls:splitRangeUrl"], symbol, range)}" +
           $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<List<SplitEntity>>();
        await _splitRepository.AddCollectionAsync(res);
    }
}