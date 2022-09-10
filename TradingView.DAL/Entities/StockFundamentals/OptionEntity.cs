namespace TradingView.DAL.Entities.StockFundamentals;
public class OptionEntity : EntityBase
{
    public string Symbol { get; set; }
    public List<string> Options { get; set; }
}
