namespace TradingView.DAL.Entities.StockProfileEntities;
public class InsiderSummaryItem
{
    public string FullName { get; set; }
    public int NetTransacted { get; set; }
    public string ReportedTitle { get; set; }
    public string Symbol { get; set; }
    public int? TotalBought { get; set; }
    public int? TotalSold { get; set; }
    public string Id { get; set; }
    public string Key { get; set; }
    public string Subkey { get; set; }
    public string Updated { get; set; }
}
