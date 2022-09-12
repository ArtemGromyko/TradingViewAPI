using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.RealTime;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Entities.RealTime;

namespace TradingView.BLL.Services.RealTime;

public class DelayedQuoteService : IDelayedQuoteService
{
    private readonly IDelayedQuoteRepository _delayedQuoteRepository;

    private readonly IConfiguration _configuration;

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _httpClient;

    public DelayedQuoteService(IDelayedQuoteRepository delayedQuoteRepository, IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _delayedQuoteRepository = delayedQuoteRepository;
        _configuration = configuration;

        _httpClientFactory = httpClientFactory;
        _httpClient = _httpClientFactory.CreateClient(_configuration["HttpClientName"]);
    }

    public async Task<DelayedQuote> GetDelayedQuoteAsync(string symbol)
    {
        var delayedQuote = await _delayedQuoteRepository.GetAsync((d) => d.Symbol!.Equals(symbol.ToUpper()));
        if (delayedQuote is null)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
                $"{string.Format(_configuration["IEXCloudUrls:delayedQuoteUrl"], symbol)}" +
                $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url);
            delayedQuote = await response.Content.ReadAsAsync<DelayedQuote>();

            await _delayedQuoteRepository.AddAsync(delayedQuote);
        }

        return delayedQuote;
    }
}
