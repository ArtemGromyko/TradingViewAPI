using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockProfile;
using TradingView.DAL.Contracts.ApiServices;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;
using TradingView.Models.Exceptions;

namespace TradingView.BLL.Services.StockProfile;
public class CEOCompensationService : ICEOCompensationService
{
    private readonly IStockProfileApiService _stockProfileApiService;
    private readonly ICEOCompensationRepository _CEOCompensationRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public CEOCompensationService(ICEOCompensationRepository CEOCompensationRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        IStockProfileApiService stockProfileApiService)
    {
        _CEOCompensationRepository = CEOCompensationRepository ?? throw new ArgumentNullException(nameof(CEOCompensationRepository));
        _stockProfileApiService = stockProfileApiService ?? throw new ArgumentNullException(nameof(stockProfileApiService));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<CEOCompensation> GetAsync(string symbol, CancellationToken ct = default)
    {
        CEOCompensation result = await _CEOCompensationRepository.GetAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (result == null)
        {
            //return await GetApiAsync(symbol, ct);
            return await _stockProfileApiService.GetCEOCompensationApiAsync(symbol, ct);
        }

        return result;
    }

    private async Task<CEOCompensation> GetApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:ceoCompensationUrl"], symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);

        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }
        var res = await response.Content.ReadAsAsync<CEOCompensation>();

        await _CEOCompensationRepository.AddAsync(res);
        return res;
    }
}
