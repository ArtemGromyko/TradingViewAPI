using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockProfile;
using TradingView.DAL.Contracts.ApiServices;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;
using TradingView.DAL.Repositories.StockProfile;
using TradingView.Models.Exceptions;

namespace TradingView.BLL.Services.StockProfile;
public class LogoService : ILogoService
{
    private readonly ILogoRepository _logoRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly IStockProfileApiService _stockProfileApiService;
    private readonly HttpClient _httpClient;

    public LogoService(ILogoRepository logoRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        IStockProfileApiService stockProfileApiService)
    {
        _logoRepository = logoRepository ?? throw new ArgumentNullException(nameof(CEOCompensationRepository));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _stockProfileApiService = stockProfileApiService ?? throw new ArgumentNullException(nameof(stockProfileApiService));
        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<Logo> GetAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _logoRepository.GetAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (result == null)
        {
            //return await GetApiAsync(symbol, ct);
            return await _stockProfileApiService.GetLogoApiAsync(symbol, ct);
        }

        return result;
    }

    private async Task<Logo> GetApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:logoUrl"], symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }
        var res = await response.Content.ReadAsAsync<Logo>();

        res.Symbol = symbol.ToUpper();
        await _logoRepository.AddAsync(res);

        return res;
    }
}
