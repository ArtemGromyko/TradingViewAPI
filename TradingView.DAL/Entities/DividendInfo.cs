namespace TradingView.DAL.Entities;


public class DividendInfo
{
    public int AdrFee { get; set; }
    public float Amount { get; set; }
    public string? AnnounceDate { get; set; }
    public string? CountryCode { get; set; }
    public int Coupon { get; set; }
    public string? Created { get; set; }
    public string? Currency { get; set; }
    public string? DeclaredCurrencyCD { get; set; }
    public string? DeclaredDate { get; set; }
    public int DeclaredGrossAmount { get; set; }
    public string? Description { get; set; }
    public string? ExDate { get; set; }
    public string? Figi { get; set; }
    public string? FiscalYearEndDate { get; set; }
    public string? Flag { get; set; }
    public string? Frequency { get; set; }
    public int FromFactor { get; set; }
    public string? FxDate { get; set; }
    public float GrossAmount { get; set; }
    public object? InstallmentPayDate { get; set; }
    public object? IsApproximate { get; set; }
    public object? IsCapitalGains { get; set; }
    public object? IsDAP { get; set; }
    public object? IsNetInvestmentIncome { get; set; }
    public string? LastUpdated { get; set; }
    public string? Marker { get; set; }
    public int NetAmount { get; set; }
    public string? Notes { get; set; }
    public string? OptionalElectionDate { get; set; }
    public float ParValue { get; set; }
    public string? ParValueCurrency { get; set; }
    public string? PaymentDate { get; set; }
    public string? PeriodEndDate { get; set; }
    public string? RecordDate { get; set; }
    public string? Refid { get; set; }
    public string? RegistrationDate { get; set; }
    public string? SecondExDate { get; set; }
    public string? SecondPaymentDate { get; set; }
    public string? SecurityType { get; set; }
    public string? Symbol { get; set; }
    public int TaxRate { get; set; }
    public string? ToDate { get; set; }
    public int ToFactor { get; set; }
    public string? Id { get; set; }
    public string? Key { get; set; }
    public string? Subkey { get; set; }
    public long Date { get; set; }
    public long Updated { get; set; }
}
