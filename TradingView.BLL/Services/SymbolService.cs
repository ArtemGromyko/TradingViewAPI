using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts;
using TradingView.DAL.Contracts;
using TradingView.DAL.Entities;

namespace TradingView.BLL.Services;

public class SymbolService : ISymbolService
{
    private readonly ISymbolRepository _symbolRepository;

    private readonly IConfiguration _configuration;

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _httpClient;

    public SymbolService(ISymbolRepository symbolRepository, IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _symbolRepository = symbolRepository;
        _configuration = configuration;

        _httpClientFactory = httpClientFactory;
        _httpClient = _httpClientFactory.CreateClient(_configuration["HttpClientName"]);
    }
    public async Task<List<SymbolInfo>> GetSymbolsAsync()
    {
        var symbols = await _symbolRepository.GetAllAsync();
        if (symbols.Count == 0)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
                $"{_configuration["IEXCloudUrls:symbolUrl"]}" +
                $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url);
            symbols = await response.Content.ReadAsAsync<List<SymbolInfo>>();

            await _symbolRepository.AddCollectionAsync(symbols);
        }

        return symbols;
    }
}
