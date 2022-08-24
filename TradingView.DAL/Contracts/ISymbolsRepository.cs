using TradingView.DAL.Entities;

namespace TradingView.DAL.Contracts;

public interface ISymbolsRepository
{
    public Task<List<SymbolInfo>> GetAllSymbolsAsync();
    public Task AddSymbolsCollection(IEnumerable<SymbolInfo> collection);
}
