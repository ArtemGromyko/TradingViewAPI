using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.RealTime;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Entities.RealTime;

namespace TradingView.BLL.Services.RealTime;

public class LargestTradesService : ILargestTradesService
{
    private readonly ILargestTradesRepository _largestTradesRepository;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public LargestTradesService(ILargestTradesRepository largestTradesRepository, IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _largestTradesRepository = largestTradesRepository;
        _configuration = configuration;

        _httpClient = httpClientFactory.CreateClient(_configuration["HttpClientName"]);
    }

    public async Task<List<LargestTrade>> GetLargestTradesListAsync(string symbol)
    {
        var largestTrades = await _largestTradesRepository.GetAllAsync();
        if (largestTrades.Count == 0)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
                $"{string.Format(_configuration["IEXCloudUrls:largestTradesUrl"], symbol)}" +
                $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url);
            var res = await response.Content.ReadAsAsync<List<LargestTrade>>();

            await _largestTradesRepository.AddCollectionAsync(res);
            largestTrades = res;
        }

        return largestTrades;
    }
}
