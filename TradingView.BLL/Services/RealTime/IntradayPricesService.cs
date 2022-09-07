using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.RealTime;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Entities.RealTime;

namespace TradingView.BLL.Services.RealTime;

public class IntradayPricesService : IIntradayPricesService
{
    private readonly IIntradayPricesRepository _intradayPricesRepository;

    private readonly IConfiguration _configuration;

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _httpClient;

    public IntradayPricesService(IIntradayPricesRepository intradayPricesRepository, IConfiguration configuration,
        IHttpClientFactory httpClientFactory, HttpClient httpClient)
    {
        _intradayPricesRepository = intradayPricesRepository;
        _configuration = configuration;

        _httpClientFactory = httpClientFactory;
        _httpClient = _httpClientFactory.CreateClient(_configuration["HttpClientName"]);
    }

    public async Task<List<IntradayPrice>> GetIntradayPricesListAsync(string symbol)
    {
        var intradayPrices = await _intradayPricesRepository.GetAllAsync();
        if (intradayPrices.Count == 0)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
                $"{string.Format(_configuration["IEXCloudUrls:intradayPricesUrl"], symbol)}" +
                $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url);
            var res = await response.Content.ReadAsAsync<List<IntradayPrice>>();

            await _intradayPricesRepository.AddCollectionAsync(res);
            intradayPrices = res;
        }

        return intradayPrices;
    }
}
