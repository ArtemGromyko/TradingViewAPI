using TradingView.DAL.Entities;

namespace TradingView.DAL.Contracts;

public interface IExchangesRepository
{
    public Task<List<ExchangeInfo>> GetAllExchangesAsync();
    public Task AddExchangesCollection(IEnumerable<ExchangeInfo> collection);
}
