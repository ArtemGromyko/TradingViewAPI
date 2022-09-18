using Microsoft.Extensions.Configuration;
using TradingView.DAL.Contracts;
using TradingView.DAL.Contracts.ApiServices;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;
using TradingView.Models.Exceptions;

namespace TradingView.DAL.ApiServices;
public class StockProfileApiService : IStockProfileApiService
{
    private readonly ISymbolRepository _symbolRepository;
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
        _symbolRepository = symbolRepository ?? throw new ArgumentNullException(nameof(symbolRepository));

        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task GetCEOCompensationApiAsync(CancellationToken ct = default)
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
                   $"{string.Format(_configuration["IEXCloudUrls:ceoCompensationUrl"], symbol.Symbol)}" +
                   $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<CEOCompensation>();
            await _ceoCompensationRepository.DeleteAsync(x => x.Symbol == symbol.Symbol, ct);
            await _ceoCompensationRepository.AddAsync(res);
        }
    }

    public async Task GetInsiderRosterApiAsync(CancellationToken ct = default)
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:insiderRosterUrl"], symbol.Symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<IEnumerable<InsiderRosterItem>>();
            await _insiderRosterRepository.DeleteAsync(x => x.Symbol == symbol.Symbol, ct);
            var roster = new InsiderRoster()
            {
                Symbol = symbol.Symbol,
                Items = res.ToList()
            };
            await _insiderRosterRepository.AddAsync(roster);
        }
    }

    public async Task GetInsiderSummaryApiAsync(CancellationToken ct = default)
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
                   $"{string.Format(_configuration["IEXCloudUrls:insiderSummaryUrl"], symbol.Symbol)}" +
                   $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<List<InsiderSummaryItem>>();
            await _insiderSummaryRepository.DeleteAsync(x => x.Symbol == symbol.Symbol, ct);
            await _insiderSummaryRepository.AddCollectionAsync(res);
        }
    }

    public async Task GetInsiderTransactionsApiAsync(CancellationToken ct = default)
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:insiderTransactionsUrl"], symbol.Symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<List<InsiderTransactionsItem>>();
            await _insiderTransactionsRepository.DeleteAsync(x => x.Symbol == symbol.Symbol, ct);
            await _insiderTransactionsRepository.AddCollectionAsync(res);
        }
    }

    public async Task GetLogoApiAsync(CancellationToken ct = default)
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:logoUrl"], symbol.Symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<Logo>();
            await _logoRepository.DeleteAsync(x => x.Symbol == symbol.Symbol, ct);
            res.Symbol = symbol.Symbol.ToUpper();
            await _logoRepository.AddAsync(res);
        }
    }

    public async Task GetPeerGroupApiAsync(CancellationToken ct = default)
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:peerGroupsUrl"], symbol.Symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<IEnumerable<string>>();
            await _peerGroupRepository.DeleteAsync(x => x.Symbol == symbol.Symbol, ct);
            var roster = new PeerGroup()
            {
                Symbol = symbol.Symbol,
                Items = res.ToList()
            };
            await _peerGroupRepository.AddAsync(roster);
        }
    }

    public async Task GetCompanyApiAsync(CancellationToken ct = default)
    {
        var symbols = await _symbolRepository.GetAllAsync();
        foreach (var symbol in symbols)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:companyUrl"], symbol.Symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException().Create(response);
            }

            var res = await response.Content.ReadAsAsync<Company>();
            await _companyRepository.DeleteAsync(x => x.Symbol == symbol.Symbol, ct);
            await _companyRepository.AddAsync(res);
        }
    }
}
