namespace TradingView.DAL.Entities.StockProfileEntities;
public class PeerGroup : EntityBase
{
    public string Symbol { get; set; }
    public List<string> Items { get; set; }
}
