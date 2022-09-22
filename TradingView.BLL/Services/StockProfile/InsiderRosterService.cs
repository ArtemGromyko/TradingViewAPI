using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockProfile;
using TradingView.DAL.Contracts.ApiServices;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;
using TradingView.Models.Exceptions;

namespace TradingView.BLL.Services.StockProfile;
public class InsiderRosterService : IInsiderRosterService
{
    private readonly IInsiderRosterRepository _insiderRosterRepository;
    private readonly IStockProfileApiService _stockProfileApiService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public InsiderRosterService(IInsiderRosterRepository insiderRosterRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        IStockProfileApiService stockProfileApiService)
    {
        _insiderRosterRepository = insiderRosterRepository ?? throw new ArgumentNullException(nameof(insiderRosterRepository));
        _stockProfileApiService = stockProfileApiService ?? throw new ArgumentNullException(nameof(stockProfileApiService));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<InsiderRoster> GetAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _insiderRosterRepository.GetAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (result == null)
        {
            //return await GetInsiderRosterApiAsync(symbol, ct);
            return await _stockProfileApiService.GetInsiderRosterApiAsync(symbol, ct);
        }

        return result;
    }

    private async Task<InsiderRoster> GetInsiderRosterApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:insiderRosterUrl"], symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<IEnumerable<InsiderRosterItem>>();

        var roster = new InsiderRoster()
        {
            Symbol = symbol,
            Items = res.ToList()
        };
        await _insiderRosterRepository.AddAsync(roster);

        return roster;
    }
}
