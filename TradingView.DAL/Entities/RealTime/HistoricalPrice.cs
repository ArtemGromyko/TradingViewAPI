namespace TradingView.DAL.Entities.RealTime;

public class HistoricalPrice : EntityBase
{
    public string? Symbol { get; set; }
    public List<HistoricalPriceItem> Items { get; set; }
}
