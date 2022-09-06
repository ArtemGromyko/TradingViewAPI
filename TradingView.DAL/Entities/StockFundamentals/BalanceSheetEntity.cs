namespace TradingView.DAL.Entities.StockFundamentals;
public class BalanceSheetEntity : EntityBase
{
    public string Symbol { get; set; }
    public List<BalanceSheetItem> BalanceSheet { get; set; }
}
