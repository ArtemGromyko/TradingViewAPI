using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockProfile;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;
using TradingView.Models.Exceptions;

namespace TradingView.BLL.Services.StockProfile;
public class InsiderSummaryService : IInsiderSummaryService
{
    private readonly IInsiderSummaryRepository _insiderSummaryRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public InsiderSummaryService(IInsiderSummaryRepository insiderSummaryRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _insiderSummaryRepository = insiderSummaryRepository ?? throw new ArgumentNullException(nameof(insiderSummaryRepository));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<List<InsiderSummaryItem>> GetAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _insiderSummaryRepository.GetCollectionAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (result.Count == 0)
        {
            return await GetCompanyApiAsync(symbol, ct);
        }

        return result;
    }

    private async Task<List<InsiderSummaryItem>> GetCompanyApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:insiderSummaryUrl"], symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }
        var res = await response.Content.ReadAsAsync<List<InsiderSummaryItem>>();

        await _insiderSummaryRepository.AddCollectionAsync(res);

        return res;
    }
}
