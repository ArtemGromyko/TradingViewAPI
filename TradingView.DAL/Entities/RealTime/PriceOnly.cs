namespace TradingView.DAL.Entities.RealTime;

public class PriceOnly : EntityBase
{
    public string? Symbol { get; set; }
    public double Price { get; set; }
}
