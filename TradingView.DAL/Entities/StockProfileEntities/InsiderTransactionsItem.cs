namespace TradingView.DAL.Entities.StockProfileEntities;
public class InsiderTransactionsItem : EntityBase
{
    public double? ConversionOrExercisePrice { get; set; }
    public char DirectIndirect { get; set; }
    public int EffectiveDate { get; set; }
    public string FilingDate { get; set; }
    public string FullName { get; set; }
    public bool Is10b51 { get; set; }
    public int PostShares { get; set; }
    public string ReportedTitle { get; set; }
    public string Symbol { get; set; }
    public char TransactionCode { get; set; }
    public string TransactionDate { get; set; }
    public double TransactionPrice { get; set; }
    public double TransactionShares { get; set; }
    public double TransactionValue { get; set; }
    public string Id { get; set; }
    public string Key { get; set; }
    public string Subkey { get; set; }
    public string Date { get; set; }
    public int Updated { get; set; }
    public double TranPrice { get; set; }
    public double TranShares { get; set; }
    public double TranValue { get; set; }
}
