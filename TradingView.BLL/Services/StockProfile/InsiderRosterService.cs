using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockProfile;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.BLL.Services.StockProfile;
public class InsiderRosterService : IInsiderRosterService
{
    private readonly IInsiderRosterRepository _insiderRosterRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public InsiderRosterService(IInsiderRosterRepository insiderRosterRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _insiderRosterRepository = insiderRosterRepository ?? throw new ArgumentNullException(nameof(insiderRosterRepository));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<InsiderRoster> GetAsync(string symbol, CancellationToken ct = default)
    {
        var company = await _insiderRosterRepository.GetAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (company == null)
        {
            return await GetCompanyApiAsync(symbol, ct);
        }

        return company;
    }

    private async Task<InsiderRoster> GetCompanyApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:insiderRosterUrl"], symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        var res = await response.Content.ReadAsAsync<IEnumerable<InsiderRosterItem>>();

        var roster = new InsiderRoster()
        {
            Symbol = symbol,
            InsiderRosterItems = res.ToList()
        };
        await _insiderRosterRepository.AddAsync(roster);

        return roster;
    }
}
