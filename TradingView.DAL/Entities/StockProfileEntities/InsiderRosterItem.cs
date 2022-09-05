namespace TradingView.DAL.Entities.StockProfileEntities;
public class InsiderRosterItem : EntityBase
{
    public string EntityName { get; set; }
    public int Position { get; set; }
    public int ReportDate { get; set; }
}
