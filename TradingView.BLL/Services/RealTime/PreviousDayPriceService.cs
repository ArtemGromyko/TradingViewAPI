using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.RealTime;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Entities.RealTime;

namespace TradingView.BLL.Services.RealTime;

public class PreviousDayPriceService : IPreviousDayPriceService
{
    private readonly IPreviousDayPriceRepository _previousDayPriceRepository;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public PreviousDayPriceService(IPreviousDayPriceRepository previousDayPriceRepository, IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _previousDayPriceRepository = previousDayPriceRepository;
        _configuration = configuration;

        _httpClient = httpClientFactory.CreateClient(_configuration["HttpClientName"]);
    }

    public async Task<PreviousDayPrice> GetPreviousDayPriceAsync(string symbol)
    {
        var previousDayPrice = await _previousDayPriceRepository.GetAsync((pdp) => true);
        if (previousDayPrice is null)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
                $"{string.Format(_configuration["IEXCloudUrls:previousDayPriceUrl"], symbol)}" +
                $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url);
            previousDayPrice = await response.Content.ReadAsAsync<PreviousDayPrice>();

            await _previousDayPriceRepository.AddAsync(previousDayPrice);
        }

        return previousDayPrice;
    }
}
