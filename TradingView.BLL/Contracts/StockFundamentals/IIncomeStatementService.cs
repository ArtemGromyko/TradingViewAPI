using TradingView.DAL.Entities.StockFundamentals;

namespace TradingView.BLL.Contracts.StockFundamentals;
public interface IIncomeStatementService
{
    Task<IncomeStatement> GetAsync(string symbol, CancellationToken ct = default);
}
