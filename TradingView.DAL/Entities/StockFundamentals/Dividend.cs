namespace TradingView.DAL.Entities.StockFundamentals;
public class Dividend
{
    public double? Amount { get; set; }
    public string Currency { get; set; }
    public DateTime? DeclaredDate { get; set; }
    public string Description { get; set; }
    public DateTime? ExDate { get; set; }
    public string Flag { get; set; }
    public double? Frequency { get; set; }
    public DateTime? PaymentDate { get; set; }
    public DateTime? RecordDate { get; set; }
    public double? Refid { get; set; }
    public string Symbol { get; set; }
    public string Id { get; set; }
    public string Key { get; set; }
    public string Subkey { get; set; }
    public double? Date { get; set; }
    public double? Updated { get; set; }
}
