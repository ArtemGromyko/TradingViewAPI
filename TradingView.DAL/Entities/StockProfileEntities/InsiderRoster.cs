namespace TradingView.DAL.Entities.StockProfileEntities;
public class InsiderRoster : EntityBase
{
    public string Symbol { get; set; }
    public List<InsiderRosterItem> InsiderRosterItems { get; set; }
}
