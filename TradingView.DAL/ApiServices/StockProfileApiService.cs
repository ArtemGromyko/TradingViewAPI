using Microsoft.Extensions.Configuration;
using TradingView.DAL.Contracts;
using TradingView.DAL.Contracts.ApiServices;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;
using TradingView.Models.Exceptions;

namespace TradingView.DAL.ApiServices;
public class StockProfileApiService : IStockProfileApiService
{
    private readonly ILogoRepository _logoRepository;
    private readonly ICEOCompensationRepository _ceoCompensationRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IInsiderRosterRepository _insiderRosterRepository;
    private readonly IInsiderSummaryRepository _insiderSummaryRepository;
    private readonly IInsiderTransactionsRepository _insiderTransactionsRepository;
    private readonly IPeerGroupRepository _peerGroupRepository;

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public StockProfileApiService(ILogoRepository logoRepository,
        ICEOCompensationRepository ceoCompensationRepository,
        ICompanyRepository companyRepository,
        IInsiderRosterRepository insiderRosterRepository,
        IInsiderSummaryRepository insiderSummaryRepository,
        IInsiderTransactionsRepository insiderTransactionsRepository,
        IPeerGroupRepository peerGroupRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        ISymbolRepository symbolRepository)
    {
        _logoRepository = logoRepository ?? throw new ArgumentNullException(nameof(logoRepository));
        _ceoCompensationRepository = ceoCompensationRepository ?? throw new ArgumentNullException(nameof(ceoCompensationRepository));
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _insiderRosterRepository = insiderRosterRepository ?? throw new ArgumentNullException(nameof(insiderRosterRepository));
        _insiderSummaryRepository = insiderSummaryRepository ?? throw new ArgumentNullException(nameof(insiderSummaryRepository));
        _insiderTransactionsRepository = insiderTransactionsRepository ?? throw new ArgumentNullException(nameof(insiderTransactionsRepository));
        _peerGroupRepository = peerGroupRepository ?? throw new ArgumentNullException(nameof(peerGroupRepository));

        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<CEOCompensation> GetCEOCompensationApiAsync(string symbol, CancellationToken ct = default)
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
        await _ceoCompensationRepository.AddAsync(res);

        return res;
    }

    public async Task<InsiderRoster> GetInsiderRosterApiAsync(string symbol, CancellationToken ct = default)
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
            Symbol = symbol.ToUpper(),
            Items = res.OrderBy(x => x.ReportDate).ToList()
        };
        await _insiderRosterRepository.DeleteAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        await _insiderRosterRepository.AddAsync(roster);

        return roster;
    }

    public async Task<List<InsiderSummaryItem>> GetInsiderSummaryApiAsync(string symbol, CancellationToken ct = default)
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
        return res.OrderBy(x => x.Updated).ToList();
    }

    public async Task<List<InsiderTransactionsItem>> GetInsiderTransactionsApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
           $"{string.Format(_configuration["IEXCloudUrls:insiderTransactionsUrl"], symbol)}" +
           $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<List<InsiderTransactionsItem>>();
        await _insiderTransactionsRepository.AddCollectionAsync(res);
        return res.OrderBy(x => x.Updated).ToList();
    }

    public async Task<Logo> GetLogoApiAsync(string symbol, CancellationToken ct = default)
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

    public async Task<PeerGroup> GetPeerGroupApiAsync(string symbol, CancellationToken ct = default)
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

    public async Task<Company> GetCompanyApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
           $"{string.Format(_configuration["IEXCloudUrls:companyUrl"], symbol)}" +
           $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException().Create(response);
        }

        var res = await response.Content.ReadAsAsync<Company>();
        await _companyRepository.AddAsync(res);

        return res;
    }
}
