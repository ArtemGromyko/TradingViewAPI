namespace TradingView.DAL.Contracts.ApiServices;
public interface IStockFundamentalsApiService
{
    Task GetBalanceSheetApiAsync(CancellationToken ct = default);
    Task GetCashFlowApiAsync(CancellationToken ct = default);
    Task GeDividendtApiAsync(string range, CancellationToken ct = default);
    Task GetEarningsApiAsync(CancellationToken ct = default);
    Task GetEarningsApiAsync(int last, CancellationToken ct = default);
    Task GetFinancialsApiAsync(CancellationToken ct = default);
    Task GetIncomeStatementApiAsync(CancellationToken ct = default);
    Task GetOptionApiAsync(CancellationToken ct = default);
    Task GetExpirationApiAsync(string expiration, CancellationToken ct = default);
    Task GetReportedFinancialsApiAsync(CancellationToken ct = default);
    Task GetSplitApiAsync(CancellationToken ct = default);
    Task GetSplitApiAsync(string range, CancellationToken ct = default);
}
