namespace TradingView.DAL.Entities.StockFundamentals;
public class CashFlowEntity : EntityBase
{
    public string Symbol { get; set; }
    public List<CashFlowItem> CashFlow { get; set; }
}
