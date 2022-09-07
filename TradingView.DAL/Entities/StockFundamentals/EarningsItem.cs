namespace TradingView.DAL.Entities.StockFundamentals;
public class EarningsItem
{
    public string EPSReportDate { get; set; }
    public double EPSSurpriseDollar { get; set; }
    public double EPSSurpriseDollarPercent { get; set; }
    public double ActualEPS { get; set; }
    public string AnnounceTime { get; set; }
    public double ConsensusEPS { get; set; }
    public string Currency { get; set; }
    public string FiscalEndDate { get; set; }
    public string fFiscalPeriod { get; set; }
    public int NumberOfEstimates { get; set; }
    public string PeriodType { get; set; }
    public string Symbol { get; set; }
    public double YearAgo { get; set; }
    public double YearAgoChangePercent { get; set; }
    public string Id { get; set; }
    public string Key { get; set; }
    public string Subkey { get; set; }
    public long Date { get; set; }
    public long Updated { get; set; }
}
