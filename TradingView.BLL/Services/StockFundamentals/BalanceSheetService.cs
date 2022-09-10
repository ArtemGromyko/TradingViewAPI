using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockFundamentals;
using TradingView.DAL.Contracts.StockFundamentals;
using TradingView.DAL.Entities.StockFundamentals;
using TradingView.Models.Exceptions;

namespace TradingView.BLL.Services.StockFundamentals;
public class BalanceSheetService : IBalanceSheetService
{
    private readonly IBalanceSheetRepository _balanceSheetRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public BalanceSheetService(IBalanceSheetRepository balanceSheetRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _balanceSheetRepository = balanceSheetRepository ?? throw new ArgumentNullException(nameof(balanceSheetRepository));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<BalanceSheetEntity> GetAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _balanceSheetRepository.GetAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (result == null)
        {
            return await GetApiAsync(symbol, ct);
        }

        return result;
    }

    private async Task<BalanceSheetEntity> GetApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:balanceSheetUrl"], symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<BalanceSheetEntity>();

        await _balanceSheetRepository.AddAsync(res);

        return res;
    }
}
