using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockFundamentals;
using TradingView.DAL.Contracts.StockFundamentals;
using TradingView.DAL.Entities.StockFundamentals;

namespace TradingView.BLL.Services.StockFundamentals;
public class CashFlowService : ICashFlowService
{
    private readonly ICashFlowRepository _cashFlowRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public CashFlowService(ICashFlowRepository cashFlowRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _cashFlowRepository = cashFlowRepository ?? throw new ArgumentNullException(nameof(cashFlowRepository));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<CashFlowEntity> GetAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _cashFlowRepository.GetAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (result == null)
        {
            return await GetApiAsync(symbol, ct);
        }

        return result;
    }

    private async Task<CashFlowEntity> GetApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:cashFlowUrl"], symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        var res = await response.Content.ReadAsAsync<CashFlowEntity>();

        await _cashFlowRepository.AddAsync(res);

        return res;
    }
}
