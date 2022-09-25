namespace TradingView.DAL.Entities.StockFundamentals;
public class IncomeStatement : EntityBase
{
    public string Symbol { get; set; }
    public List<Income> Income { get; set; }
}
