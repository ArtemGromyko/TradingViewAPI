using TradingView.DAL.Entities;

namespace TradingView.BLL.Contracts;

public interface IExchangeService
{
    Task<List<ExchangeInfo>> GetExchangesAsync();
}
