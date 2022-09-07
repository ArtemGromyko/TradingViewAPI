using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.RealTime;

namespace TradingView.BLL.Services.RealTime;

public class PriceOnlyService : IPriceOnlyService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public PriceOnlyService(IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;

        _httpClient = httpClientFactory.CreateClient(_configuration["HttpClientName"]);
    }

    public async Task<double> GetPriceOnlyAsync(string symbol)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
                $"{string.Format(_configuration["IEXCloudUrls:priceOnlyUrl"], symbol)}" +
                $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url);
        var price = await response.Content.ReadAsAsync<double>();

        return price;
    }
}
