namespace TradingView.DAL.Entities.StockFundamentals;
public class Expiration //: EntityBase
{
    public double Ask { get; set; }
    public double Bid { get; set; }
    public string CfiCode { get; set; }
    public double Close { get; set; }
    public double ClosingPrice { get; set; }
    public string ContractDescription { get; set; }
    public string ContractName { get; set; }
    public int ContractSize { get; set; }
    public string Currency { get; set; }
    public double? ExchangeCode { get; set; }
    public double? ExchangeMIC { get; set; }
    public string ExerciseStyle { get; set; }
    public string ExpirationDate { get; set; }
    public string Figi { get; set; }
    public double High { get; set; }
    public bool IsAdjusted { get; set; }
    public DateTime LastTradeDate { get; set; }
    public DateTime LastTradeTime { get; set; }
    public DateTime LastUpdated { get; set; }
    public double Low { get; set; }
    public double MarginPrice { get; set; }
    public double Open { get; set; }
    public long OpenInterest { get; set; }
    public double SettlementPrice { get; set; }
    public string Side { get; set; }
    public double StrikePrice { get; set; }
    public string Symbol { get; set; }
    public string Type { get; set; }
    public double Volume { get; set; }
    public string Id { get; set; }
    public string Key { get; set; }
    public string Subkey { get; set; }
    public long Date { get; set; }
    public long Updated { get; set; }
}
