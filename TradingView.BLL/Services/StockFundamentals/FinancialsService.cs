using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockFundamentals;
using TradingView.DAL.Contracts.StockFundamentals;
using TradingView.DAL.Entities.StockFundamentals;

namespace TradingView.BLL.Services.StockFundamentals;
public class FinancialsService : IFinancialsService
{
    private readonly IFinancialsRepository _financialsRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public FinancialsService(IFinancialsRepository financialsRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _financialsRepository = financialsRepository ?? throw new ArgumentNullException(nameof(financialsRepository));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<FinancialsEntity> GetAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _financialsRepository.GetAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (result == null)
        {
            return await GetApiAsync(symbol, ct);
        }

        return result;
    }

    private async Task<FinancialsEntity> GetApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:financialsUrl"], symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        var res = await response.Content.ReadAsAsync<FinancialsEntity>();

        await _financialsRepository.AddAsync(res);

        return res;
    }
}
