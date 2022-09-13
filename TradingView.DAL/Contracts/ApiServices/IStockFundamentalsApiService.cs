using TradingView.DAL.Entities.StockFundamentals;

namespace TradingView.DAL.Contracts.ApiServices;
public interface IStockFundamentalsApiService
{
    Task<BalanceSheetEntity> GetBalanceSheetApiAsync(CancellationToken ct = default);
    Task<CashFlowEntity> GetCashFlowApiAsync(CancellationToken ct = default);
    Task<List<Dividend>> GeDividendtApiAsync(string range, CancellationToken ct = default);
    Task<EarningsEntity> GetEarningsApiAsync(CancellationToken ct = default);
    Task<EarningsEntity> GetEarningsApiAsync(int last, CancellationToken ct = default);
    Task<FinancialsEntity> GetFinancialsApiAsync(CancellationToken ct = default);
    Task<IncomeStatement> GetIncomeStatementApiAsync(CancellationToken ct = default);
    Task<OptionEntity> GetOptionApiAsync(CancellationToken ct = default);
    Task<List<Expiration>> GetExpirationApiAsync(string expiration, CancellationToken ct = default);
    Task<List<ReportedFinancials>> GetReportedFinancialsApiAsync(CancellationToken ct = default);
    Task<List<SplitEntity>> GetSplitApiAsync(CancellationToken ct = default);
    Task<List<SplitEntity>> GetSplitApiAsync(string range, CancellationToken ct = default);
}
