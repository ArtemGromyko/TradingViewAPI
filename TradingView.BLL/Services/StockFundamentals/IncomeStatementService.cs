using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockFundamentals;
using TradingView.DAL.Contracts.StockFundamentals;
using TradingView.DAL.Entities.StockFundamentals;
using TradingView.Models.Exceptions;

namespace TradingView.BLL.Services.StockFundamentals;
public class IncomeStatementService : IIncomeStatementService
{
    private readonly IIncomeStatementRepository _incomeStatementRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public IncomeStatementService(IIncomeStatementRepository incomeStatementRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _incomeStatementRepository = incomeStatementRepository ?? throw new ArgumentNullException(nameof(incomeStatementRepository));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<IncomeStatement> GetAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _incomeStatementRepository.GetAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (result == null)
        {
            return await GetApiAsync(symbol, ct);
        }

        return result;
    }

    private async Task<IncomeStatement> GetApiAsync(string symbol, CancellationToken ct = default)
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
}
