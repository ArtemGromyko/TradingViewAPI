namespace TradingView.DAL.Contracts.ApiServices;
public interface IStockFundamentalsApiService
{
    Task GetBalanceSheetApiAsync(string symbol, CancellationToken ct = default);
    Task GetCashFlowApiAsync(string symbol, CancellationToken ct = default);
    Task GeDividendtApiAsync(string symbol, string range, CancellationToken ct = default);
    Task GetEarningsApiAsync(string symbol, CancellationToken ct = default);
    Task GetEarningsApiAsync(string symbol, int last, CancellationToken ct = default);
    Task GetFinancialsApiAsync(string symbol, CancellationToken ct = default);
    Task GetIncomeStatementApiAsync(string symbol, CancellationToken ct = default);
    Task GetOptionApiAsync(string symbol, CancellationToken ct = default);
    Task GetExpirationApiAsync(string symbol, string expiration, CancellationToken ct = default);
    Task GetReportedFinancialsApiAsync(string symbol, CancellationToken ct = default);
    Task GetSplitApiAsync(string symbol, CancellationToken ct = default);
    Task GetSplitApiAsync(string symbol, string range, CancellationToken ct = default);
}
