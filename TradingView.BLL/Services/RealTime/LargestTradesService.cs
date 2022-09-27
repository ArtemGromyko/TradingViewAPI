using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.RealTime;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Entities.RealTime.LargestTrade;
using TradingView.Models.Exceptions;

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

    public async Task<List<LargestTradeItem>> GetLargestTradesListAsync(string symbol)
    {
        var largestTrade = await _largestTradesRepository.GetAsync((lt) => lt.Symbol!.ToUpper() == symbol.ToUpper());
        if (largestTrade is null)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
                $"{string.Format(_configuration["IEXCloudUrls:largestTradesUrl"], symbol)}" +
                $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var largestTradeItems = await response.Content.ReadAsAsync<List<LargestTradeItem>>();

            var newLargestTrade = new LargestTrade { Symbol = symbol, Items = largestTradeItems };
            await _largestTradesRepository.AddAsync(newLargestTrade);

            return largestTradeItems;
        }

        return largestTrade.Items!;
    }
}
