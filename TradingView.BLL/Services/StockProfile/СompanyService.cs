using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TradingView.BLL.Contracts.StockProfile;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities;
using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.BLL.Services.StockProfile;
public class СompanyService : IСompanyService
{


    private readonly ICompanyRepository _companyRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public СompanyService(ICompanyRepository companyRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<Company> GetCompanyAsync(string symbol, CancellationToken ct = default)
    {
        var company = await _companyRepository.GetAsync(x => x.Symbol == symbol, ct);
        if (company == null)
        {
            return await GetCompanyApiAsync(symbol, ct);
        }

        return company;
    }

    private async Task<Company> GetCompanyApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:companyUrl"], symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url);
        var res = await response.Content.ReadAsAsync<Company>();

        await _companyRepository.AddAsync(res);

        return res;
    }
}