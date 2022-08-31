using TradingView.DAL.Entities.RealTime;

namespace TradingView.BLL.Contracts.RealTime;

public interface IQuotesService
{
    Task<Quote> GetQuoteAsync(string symbol);
}
