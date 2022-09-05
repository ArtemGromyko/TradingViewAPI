﻿using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.StockProfile;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.BLL.Services.StockProfile;
public class InsiderTransactionsService : IInsiderTransactionsService
{
    private readonly IInsiderTransactionsRepository _insiderTransactionsRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public InsiderTransactionsService(IInsiderTransactionsRepository insiderTransactionsRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _insiderTransactionsRepository = insiderTransactionsRepository ?? throw new ArgumentNullException(nameof(insiderTransactionsRepository));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _httpClient = _httpClientFactory.CreateClient(configuration["HttpClientName"]);
    }

    public async Task<List<InsiderTransactionsItem>> GetAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _insiderTransactionsRepository.GetCollectionAsync(x => x.Symbol.ToUpper() == symbol.ToUpper(), ct);
        if (result.Count == 0)
        {
            return await GetCompanyApiAsync(symbol, ct);
        }

        return result;
    }

    private async Task<List<InsiderTransactionsItem>> GetCompanyApiAsync(string symbol, CancellationToken ct = default)
    {
        var url = $"{_configuration["IEXCloudUrls:version"]}" +
               $"{string.Format(_configuration["IEXCloudUrls:insiderTransactionsUrl"], symbol)}" +
               $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

        var response = await _httpClient.GetAsync(url, ct);
        var res = await response.Content.ReadAsAsync<List<InsiderTransactionsItem>>();

        await _insiderTransactionsRepository.AddCollectionAsync(res);

        return res;
    }
}
