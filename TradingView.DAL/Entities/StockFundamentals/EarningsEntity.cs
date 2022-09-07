namespace TradingView.DAL.Entities.StockFundamentals;
public class EarningsEntity : EntityBase
{
    public string Symbol { get; set; }
    public List<EarningsItem> Earnings { get; set; }
}
