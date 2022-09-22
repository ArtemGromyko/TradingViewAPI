using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockProfile;
using TradingView.DAL.Contracts.ApiServices;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;
using TradingView.Models.Exceptions;

namespace TradingView.BLL.Services.StockProfile;
public class PeerGroupService : IPeerGroupService
{
    private readonly IPeerGroupRepository _peerGroupRepository;
    private readonly IStockProfileApiService _stockProfileApiService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public PeerGroupService(IPeerGroupRepository peerGroupRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        IStockProfileApiService stockProfileApiService)
    {
        _peerGroupRepository = peerGroupRepository ?? throw new ArgumentNullException(nameof(peerGroupRepository));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _stockProfileApiService = stockProfileApiService ?? throw new ArgumentNullException(nameof(stockProfileApiService));
        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<PeerGroup> GetAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _peerGroupRepository.GetAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (result == null)
        {
            //return await GetCompanyApiAsync(symbol, ct);
            return await _stockProfileApiService.GetPeerGroupApiAsync(symbol, ct);
        }

        return result;
    }

    private async Task<PeerGroup> GetCompanyApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:peerGroupsUrl"], symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }
        var res = await response.Content.ReadAsAsync<IEnumerable<string>>();

        var roster = new PeerGroup()
        {
            Symbol = symbol,
            Items = res.ToList()
        };
        await _peerGroupRepository.AddAsync(roster);

        return roster;
    }
}
