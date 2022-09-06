using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.BLL.Contracts.StockProfile;
public interface IPeerGroupService
{
    Task<PeerGroup> GetAsync(string symbol, CancellationToken ct = default);
}
