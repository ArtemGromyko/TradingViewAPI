namespace TradingView.DAL.Entities.RealTime;


public class IntradayPrice : EntityBase
{
    public string? Date { get; set; }
    public string? Minute { get; set; }
    public string? Label { get; set; }
    public float High { get; set; }
    public float Low { get; set; }
    public float Open { get; set; }
    public float Close { get; set; }
    public decimal? Average { get; set; }
    public decimal? Volume { get; set; }
    public decimal? Notional { get; set; }
    public int? NumberOfTrades { get; set; }
}

