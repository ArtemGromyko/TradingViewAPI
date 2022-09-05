using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.RealTime;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Entities.RealTime;

namespace TradingView.BLL.Services.RealTime;

public class HistoricalPricesService : IHistoricalPricesService
{
    private readonly IHistoricalPricesRepository _historicalPricesRepository;

    private readonly IConfiguration _configuration;

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _httpClient;

    public HistoricalPricesService(IHistoricalPricesRepository historicalPricesRepository, IConfiguration configuration,
        IHttpClientFactory httpClientFactory, HttpClient httpClient)
    {
        _historicalPricesRepository = historicalPricesRepository;
        _configuration = configuration;

        _httpClientFactory = httpClientFactory;
        _httpClient = _httpClientFactory.CreateClient(_configuration["HttpClientName"]);
    }

    public async Task<List<HistoricalPrice>> GetHistoricalPricesListAsync(string symbol)
    {
        var historicalPrices = await _historicalPricesRepository.GetAllAsync();
        if (historicalPrices.Count == 0)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
                $"{string.Format(_configuration["IEXCloudUrls:historicalPricesUrl"], symbol)}" +
                $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url);
            var res = await response.Content.ReadAsAsync<IEnumerable<HistoricalPrice>>();

            await _historicalPricesRepository.AddCollectionAsync(res);
            historicalPrices = res.ToList();
        }

        return historicalPrices;
    }
}
