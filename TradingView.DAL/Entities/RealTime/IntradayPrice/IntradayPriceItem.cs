namespace TradingView.DAL.Entities.RealTime.IntradayPrice;

public class IntradayPriceItem
{
    public string? Date { get; set; }
    public string? Minute { get; set; }
    public string? Label { get; set; }
    public float High { get; set; }
    public float Low { get; set; }
    public float Open { get; set; }
    public float Close { get; set; }
    public double? Average { get; set; }
    public double? Volume { get; set; }
    public double? Notional { get; set; }
    public int? NumberOfTrades { get; set; }
}
