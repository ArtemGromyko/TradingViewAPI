using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockFundamentals;
using TradingView.DAL.Contracts.StockFundamentals;
using TradingView.DAL.Entities.StockFundamentals;
using TradingView.Models.Exceptions;

namespace TradingView.BLL.Services.StockFundamentals;
public class ReportedFinancialsService : IReportedFinancialsService
{
    private readonly IReportedFinancialsRepository _reportedFinancialsRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public ReportedFinancialsService(IReportedFinancialsRepository reportedFinancialsRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _reportedFinancialsRepository = reportedFinancialsRepository ?? throw new ArgumentNullException(nameof(reportedFinancialsRepository));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<List<ReportedFinancials>> GetAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _reportedFinancialsRepository.GetCollectionAsync(x => x.Key.ToUpper() == symbol.ToUpper(), ct);
        if (result.Count == 0)
        {
            return await GetApiAsync(symbol, ct);
        }

        return result;
    }

    private async Task<List<ReportedFinancials>> GetApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:reportedFinancialsUrl"], symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<List<ReportedFinancials>>();

        await _reportedFinancialsRepository.AddCollectionAsync(res);

        return res;
    }
}
