using TradingView.DAL.Entities;

namespace TradingView.DAL.Contracts;

public interface IDividendsRepository
{
    public Task<List<DividendInfo>> GetAllDividendsAsync();
    public Task AddDividendsCollection(IEnumerable<DividendInfo> collection);
}
