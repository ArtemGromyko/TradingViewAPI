using MongoDB.Bson.Serialization.Attributes;

namespace TradingView.DAL.Entities.RealTime;


[BsonNoId]
public class Quote : EntityBase
{
    public int AvgTotalVolume { get; set; }
    public string? CalculationPrice { get; set; }
    public float Change { get; set; }
    public float ChangePercent { get; set; }
    public double Close { get; set; }
    public string? CloseSource { get; set; }
    public decimal? CloseTime { get; set; }
    public string? CompanyName { get; set; }
    public string? Currency { get; set; }
    public decimal? DelayedPrice { get; set; }
    public decimal? DelayedPriceTime { get; set; }
    public decimal? ExtendedChange { get; set; }
    public decimal? ExtendedChangePercent { get; set; }
    public decimal? ExtendedPrice { get; set; }
    public decimal? ExtendedPriceTime { get; set; }
    public double High { get; set; }
    public string? HighSource { get; set; }
    public decimal? HighTime { get; set; }
    public decimal? IexAskPrice { get; set; }
    public int? IexAskSize { get; set; }
    public decimal? IexBidPrice { get; set; }
    public int? IexBidSize { get; set; }
    public float IexClose { get; set; }
    public long IexCloseTime { get; set; }
    public long? IexLastUpdated { get; set; }
    public float? IexMarketPercent { get; set; }
    public float? IexOpen { get; set; }
    public long? IexOpenTime { get; set; }
    public float? IexRealtimePrice { get; set; }
    public int? IexRealtimeSize { get; set; }
    public int? IexVolume { get; set; }
    public long? LastTradeTime { get; set; }
    public float? LatestPrice { get; set; }
    public string? LatestSource { get; set; }
    public string? LatestTime { get; set; }
    public long? LatestUpdate { get; set; }
    public decimal? LatestVolume { get; set; }
    public double? Low { get; set; }
    public string? LowSource { get; set; }
    public long? LowTime { get; set; }
    public long? MarketCap { get; set; }
    public decimal? OddLotDelayedPrice { get; set; }
    public decimal? OddLotDelayedPriceTime { get; set; }
    public double? Open { get; set; }
    public decimal? OpenTime { get; set; }
    public string? OpenSource { get; set; }
    public float? PeRatio { get; set; }
    public float? PreviousClose { get; set; }
    public int? PreviousVolume { get; set; }
    public string? PrimaryExchange { get; set; }
    public string? Symbol { get; set; }
    public decimal? Volume { get; set; }
    public float? Week52High { get; set; }
    public float? Week52Low { get; set; }
    public float? YtdChange { get; set; }
    public bool? IsUSMarketOpen { get; set; }
}
