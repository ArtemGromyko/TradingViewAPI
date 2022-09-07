using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.RealTime;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Entities.RealTime;

namespace TradingView.BLL.Services.RealTime;

public class QuotesService : IQuotesService
{
    private readonly IQuotesRepository _quotesRepository;

    private readonly IConfiguration _configuration;

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _httpClient;

    public QuotesService(IQuotesRepository quotesRepository, IConfiguration configuration,
        IHttpClientFactory httpClientFactory, HttpClient httpClient)
    {
        _quotesRepository = quotesRepository;
        _configuration = configuration;

        _httpClientFactory = httpClientFactory;
        _httpClient = _httpClientFactory.CreateClient(configuration["httpClientName"]);
    }

    public async Task<Quote> GetQuoteAsync(string symbol)
    {
        var quote = await _quotesRepository.GetAsync((q) => q.Symbol!.Equals(symbol));
        if (quote is null)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
                $"{string.Format(_configuration["IEXCloudUrls:quotesUrl"], symbol)}" +
                $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url);
            quote = await response.Content.ReadAsAsync<Quote>();

            await _quotesRepository.AddAsync(quote);
        }

        return quote;
    }
}
