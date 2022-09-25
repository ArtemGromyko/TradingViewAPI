using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockFundamentals;
using TradingView.DAL.Contracts.StockFundamentals;
using TradingView.DAL.Entities.StockFundamentals;
using TradingView.Models.Exceptions;

namespace TradingView.BLL.Services.StockFundamentals;
public class OptionService : IOptionService
{
    private readonly IOptionRepository _optionRepository;
    private readonly IExpirationRepository _expirationRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public OptionService(IOptionRepository optionRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        IExpirationRepository expirationRepository)
    {
        _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _expirationRepository = expirationRepository ?? throw new ArgumentNullException(nameof(expirationRepository));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<OptionEntity> GetOptionAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _optionRepository.GetAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (result == null)
        {
            return await GetApiAsync(symbol, ct);
        }

        return result;
    }

    public async Task<List<Expiration>> GetExpirationAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _expirationRepository.GetCollectionAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (result == null)
        {
            await GetApiAsync(symbol, ct);
            result = await _expirationRepository.GetCollectionAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        }

        return result;
    }

    private async Task<OptionEntity> GetApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:optionUrl"], symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<List<string>>();
        var option = new OptionEntity()
        {
            Symbol = symbol,
            Options = res
        };
        await _optionRepository.AddAsync(option);

        foreach (var temp in res)
        {
            try
            {
                await GetApiAsync(symbol, temp, ct);
            }
            catch { }
        }

        return option;
    }

    private async Task<List<Expiration>> GetApiAsync(string symbol, string expiration, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:optionExpirationUrl"], symbol, expiration)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<List<Expiration>>();

        await _expirationRepository.AddCollectionAsync(res);

        return res;
    }
}
