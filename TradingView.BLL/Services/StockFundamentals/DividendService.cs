using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockFundamentals;
using TradingView.DAL.Contracts.StockFundamentals;
using TradingView.DAL.Entities.StockFundamentals;
using TradingView.Models.Exceptions;

namespace TradingView.BLL.Services.StockFundamentals;
public class DividendService : IDividendService
{
    private readonly IDividendRepository _dividendRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public DividendService(IDividendRepository dividendRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _dividendRepository = dividendRepository ?? throw new ArgumentNullException(nameof(dividendRepository));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<List<Dividend>> GetAsync(string symbol, string range, CancellationToken ct = default)
    {
        var result = await _dividendRepository.GetCollectionAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (result.Count == 0)
        {
            return await GetApiAsync(symbol, range ?? "5y", ct);
        }

        return result;
    }

    private async Task<List<Dividend>> GetApiAsync(string symbol, string range, CancellationToken ct = default)
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

        return res;
    }
}
