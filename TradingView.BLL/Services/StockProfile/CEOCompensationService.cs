using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockProfile;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.BLL.Services.StockProfile;
public class CEOCompensationService : ICEOCompensationService
{
    private readonly ICEOCompensationRepository _CEOCompensationRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public CEOCompensationService(ICEOCompensationRepository CEOCompensationRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _CEOCompensationRepository = CEOCompensationRepository ?? throw new ArgumentNullException(nameof(CEOCompensationRepository));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<CEOCompensation> GetAsync(string symbol, CancellationToken ct = default)
    {
        var company = await _CEOCompensationRepository.GetAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (company == null)
        {
            return await GetApiAsync(symbol, ct);
        }

        return company;
    }

    private async Task<CEOCompensation> GetApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:ceoCompensationUrl"], symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        var res = await response.Content.ReadAsAsync<CEOCompensation>();

        await _CEOCompensationRepository.AddAsync(res);

        return res;
    }
}
