using TradingView.DAL.Entities.StockFundamentals;

namespace TradingView.BLL.Contracts.StockFundamentals;
public interface IReportedFinancialsService
{
    Task<List<ReportedFinancials>> GetAsync(string symbol, CancellationToken ct = default);
}
