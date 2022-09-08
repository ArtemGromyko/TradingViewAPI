using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockFundamentals;
using TradingView.DAL.Contracts.StockFundamentals;
using TradingView.DAL.Entities.StockFundamentals;

namespace TradingView.BLL.Services.StockFundamentals;
public class SplitService : ISplitService
{
    private readonly ISplitRepository _splitRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public SplitService(ISplitRepository splitRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _splitRepository = splitRepository ?? throw new ArgumentNullException(nameof(splitRepository));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<List<SplitEntity>> GetAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _splitRepository.GetCollectionAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (result.Count == 0)
        {
            return await GetApiAsync(symbol, ct);
        }

        return result;
    }

    private async Task<List<SplitEntity>> GetApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:splitUrl"], symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        var res = await response.Content.ReadAsAsync<List<SplitEntity>>();

        await _splitRepository.AddCollectionAsync(res);

        return res;
    }
}
