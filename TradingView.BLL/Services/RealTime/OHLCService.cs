﻿using Microsoft.Extensions.Configuration;
using TradingView.BLL.Contracts.RealTime;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Entities.RealTime.OHLC;

namespace TradingView.BLL.Services.RealTime;

public class OHLCService : IOHLCService
{
    private readonly IOHLCRepository _ohlcRepository;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public OHLCService(IOHLCRepository ohlcRepository, IConfiguration configuration,
        IHttpClientFactory httpClientFactory, HttpClient httpClient)
    {
        _ohlcRepository = ohlcRepository;
        _configuration = configuration;

        _httpClient = httpClientFactory.CreateClient(_configuration["HttpClientName"]);
    }

    public async Task<OHLC> GetOHLCAsync(string symbol)
    {
        var ohlc = await _ohlcRepository.GetAsync((ohlc) => true);
        if (ohlc is null)
        {
            var url = $"{_configuration["IEXCloudUrls:version"]}" +
                $"{string.Format(_configuration["IEXCloudUrls:ohlcUrl"], symbol)}" +
                $"?token={Environment.GetEnvironmentVariable("PUBLISHABLE_TOKEN")}";

            var response = await _httpClient.GetAsync(url);
            ohlc = await response.Content.ReadAsAsync<OHLC>();

            await _ohlcRepository.AddAsync(ohlc);
        }

        return ohlc;
    }
}
