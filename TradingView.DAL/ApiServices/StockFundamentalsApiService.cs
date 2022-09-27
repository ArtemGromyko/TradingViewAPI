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

    public async Task<BalanceSheetEntity> GetBalanceSheetApiAsync(string symbol, CancellationToken ct = default)
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

        return res;
    }


    public async Task<CashFlowEntity> GetCashFlowApiAsync(string symbol, CancellationToken ct = default)
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

        return res;
    }


    public async Task<List<Dividend>> GeDividendtApiAsync(string symbol, string range, CancellationToken ct = default)
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
        if(res.Count != 0)
        {
            await _dividendRepository.AddCollectionAsync(res);
        }
        

        return res;
    }

    public async Task<EarningsEntity> GetEarningsApiAsync(string symbol, CancellationToken ct = default)
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

        return res;
    }

    public async Task<EarningsEntity> GetEarningsApiAsync(string symbol, int last, CancellationToken ct = default)
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

        return res;
    }

    public async Task<FinancialsEntity> GetFinancialsApiAsync(string symbol, CancellationToken ct = default)
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

        return res;
    }

    public async Task<IncomeStatement> GetIncomeStatementApiAsync(string symbol, CancellationToken ct = default)
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

        return res;
    }

    public async Task<OptionEntity> GetOptionApiAsync(string symbol, CancellationToken ct = default)
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

        return option;
    }

    public async Task<List<Expiration>> GetExpirationApiAsync(string symbol, string expiration, CancellationToken ct = default)
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
        
        if(res.Count != 0)
        {
            await _expirationRepository.AddCollectionAsync(res);
        }

        return res;
    }

    public async Task<List<ReportedFinancials>> GetReportedFinancialsApiAsync(string symbol, CancellationToken ct = default)
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
        
        if(res.Count != 0)
        {
            await _reportedFinancialsRepository.AddCollectionAsync(res);
        }

        return res;
    }

    public async Task<List<SplitEntity>> GetSplitApiAsync(string symbol, CancellationToken ct = default)
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
        
        if(res.Count != 0)
        {
            await _splitRepository.AddCollectionAsync(res);
        }

        return res;
    }


    public async Task<List<SplitEntity>> GetSplitApiAsync(string symbol, string range, CancellationToken ct = default)
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
        
        if(res.Count != 0)
        {
            await _splitRepository.AddCollectionAsync(res);
        }

        return res;
    }
}