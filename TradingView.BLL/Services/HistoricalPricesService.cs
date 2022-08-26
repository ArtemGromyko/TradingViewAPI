using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts;
using TradingView.DAL.Contracts;
using TradingView.DAL.Entities;

namespace TradingView.BLL.Services;

public class HistoricalPricesService : IHistoricalPricesService
{
    private readonly IDividendsRepository _dividendsRepository;
    private readonly ISymbolsRepository _symbolsRepository;
    private readonly IExchangesRepository _exchangesRepository;
    private readonly IHistoricalPricesRepository _historicalPricesRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _httpClient;

    public HistoricalPricesService(IDividendsRepository dividendsRepository,
    ISymbolsRepository symbolsRepository, IExchangesRepository exchangesRepository,
    IHistoricalPricesRepository historicalPricesRepository, IHttpClientFactory httpClientFactory,
    IConfiguration configuration)
    {
        _dividendsRepository = dividendsRepository;
        _symbolsRepository = symbolsRepository;
        _exchangesRepository = exchangesRepository;
        _historicalPricesRepository = historicalPricesRepository;
        _httpClientFactory = httpClientFactory;

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public Task<List<DividendInfo>> GetAllDividendsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<ExchangeInfo>> GetAllExchanges()
    {
        throw new NotImplementedException();
    }

    public Task<List<HistoricalPrice>> GetAllHistoricalPricesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<SymbolInfo>> GetAllSymbolsAsync()
    {
        throw new NotImplementedException();
    }
}
