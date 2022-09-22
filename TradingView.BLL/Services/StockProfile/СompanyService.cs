using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockProfile;
using TradingView.DAL.Contracts.ApiServices;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;
using TradingView.Models.Exceptions;

namespace TradingView.BLL.Services.StockProfile;
public class СompanyService : IСompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IStockProfileApiService _stockProfileApiService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public СompanyService(ICompanyRepository companyRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        IStockProfileApiService stockProfileApiService)
    {
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _stockProfileApiService = stockProfileApiService ?? throw new ArgumentNullException(nameof(stockProfileApiService));
        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<Company> GetAsync(string symbol, CancellationToken ct = default)
    {
        var company = await _companyRepository.GetAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (company == null)
        {
           // return await GetCompanyApiAsync(symbol, ct);
            return await _stockProfileApiService.GetCompanyApiAsync(symbol, ct);
        }

        return company;
    }

    private async Task<Company> GetCompanyApiAsync(string symbol, CancellationToken ct = default)
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