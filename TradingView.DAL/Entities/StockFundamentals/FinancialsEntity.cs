namespace TradingView.DAL.Entities.StockFundamentals;
public class FinancialsEntity : EntityBase
{
    public string Symbol { get; set; }
    public List<FinancialsItem> Financials { get; set; }
}
