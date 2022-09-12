using TradingView.DAL.Entities.StockFundamentals;

namespace TradingView.DAL.Contracts.ApiServices;
public interface IStockFundamentalsApiService
{
    Task<BalanceSheetEntity> GetBalanceSheetApiAsync(string symbol, CancellationToken ct = default);
    Task<CashFlowEntity> GetCashFlowApiAsync(string symbol, CancellationToken ct = default);
    Task<List<Dividend>> GeDividendtApiAsync(string symbol, string range, CancellationToken ct = default);
    Task<EarningsEntity> GetEarningsApiAsync(string symbol, CancellationToken ct = default);
    Task<EarningsEntity> GetEarningsApiAsync(string symbol, int last, CancellationToken ct = default);
    Task<FinancialsEntity> GetFinancialsApiAsync(string symbol, CancellationToken ct = default);
    Task<IncomeStatement> GetIncomeStatementApiAsync(string symbol, CancellationToken ct = default);
    Task<OptionEntity> GetOptionApiAsync(string symbol, CancellationToken ct = default);
    Task<List<Expiration>> GetExpirationApiAsync(string symbol, string expiration, CancellationToken ct = default);
    Task<List<ReportedFinancials>> GetReportedFinancialsApiAsync(string symbol, CancellationToken ct = default);
    Task<List<SplitEntity>> GetSplitApiAsync(string symbol, CancellationToken ct = default);
    Task<List<SplitEntity>> GetSplitApiAsync(string symbol, string range, CancellationToken ct = default);
}
