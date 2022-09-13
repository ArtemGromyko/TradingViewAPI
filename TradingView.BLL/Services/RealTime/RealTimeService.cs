using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.RealTime;
using TradingView.DAL.Contracts;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Entities.RealTime;

namespace TradingView.BLL.Services.RealTime;

public class RealTimeService : IRealTimeService
{
    private readonly IDividendsRepository _dividendsRepository;
    private readonly IExchangesRepository _exchangesRepository;
    private readonly IHistoricalPricesRepository _historicalPricesRepository;

    private readonly IConfiguration _configuration;

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _httpClient;

    public RealTimeService(IDividendsRepository dividendsRepository,
    IExchangesRepository exchangesRepository,
    IHistoricalPricesRepository historicalPricesRepository, IHttpClientFactory httpClientFactory,
    IConfiguration configuration)
    {
        _configuration = configuration;

        _dividendsRepository = dividendsRepository;
        _exchangesRepository = exchangesRepository;
        _historicalPricesRepository = historicalPricesRepository;
        _httpClientFactory = httpClientFactory;

        _httpClient = _httpClientFactory.CreateClient(_configuration["HttpClientName"]);
    }

    public Task<List<DividendInfo>> GetAllDividendsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<ExchangeInfo>> GetAllExchanges()
    {
        throw new NotImplementedException();
    }

    public async Task<List<HistoricalPrice>> GetAllHistoricalPricesAsync(string symbol)
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
