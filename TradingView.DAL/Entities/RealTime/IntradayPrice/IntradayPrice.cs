namespace TradingView.DAL.Entities.RealTime.IntradayPrice;

public class IntradayPrice : EntityBase
{
    public string? Symbol { get; set; }
    public List<IntradayPriceItem>? Items { get; set; }
}
