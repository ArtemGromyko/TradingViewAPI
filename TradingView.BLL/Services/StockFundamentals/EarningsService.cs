using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockFundamentals;
using TradingView.DAL.Contracts.StockFundamentals;
using TradingView.DAL.Entities.StockFundamentals;

namespace TradingView.BLL.Services.StockFundamentals;
public class EarningsService : IEarningsService
{
    private readonly IEarningsRepository _earningsRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public EarningsService(IEarningsRepository earningsRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _earningsRepository = earningsRepository ?? throw new ArgumentNullException(nameof(earningsRepository));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<EarningsEntity> GetAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _earningsRepository.GetAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (result == null)
        {
            return await GetApiAsync(symbol, ct);
        }

        return result;
    }

    private async Task<EarningsEntity> GetApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:earningsUrl"], symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        var res = await response.Content.ReadAsAsync<EarningsEntity>();

        await _earningsRepository.AddAsync(res);

        return res;
    }
}
