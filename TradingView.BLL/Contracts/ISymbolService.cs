using TradingView.DAL.Entities;

namespace TradingView.BLL.Contracts;

public interface ISymbolService
{
    Task<List<SymbolInfo>> GetSymbolsAsync();
}
