using TradingView.DAL.Entities.RealTime;

namespace TradingView.BLL.Contracts.RealTime;

public interface IDelayedQuoteService
{
    Task<DelayedQuote> GetDelayedQuoteAsync(string symbol);
}
