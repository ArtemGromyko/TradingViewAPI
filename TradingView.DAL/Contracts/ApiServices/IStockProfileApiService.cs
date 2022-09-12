using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.DAL.Contracts.ApiServices;
public interface IStockProfileApiService
{
    Task<CEOCompensation> GetCEOCompensationApiAsync(string symbol, CancellationToken ct = default);
    Task<InsiderRoster> GetInsiderRosterApiAsync(string symbol, CancellationToken ct = default);
    Task<List<InsiderSummaryItem>> GetInsiderSummaryApiAsync(string symbol, CancellationToken ct = default);
    Task<List<InsiderTransactionsItem>> GetInsiderTransactionsApiAsync(string symbol, CancellationToken ct = default);
    Task<Logo> GetLogoApiAsync(string symbol, CancellationToken ct = default);
    Task<PeerGroup> GetPeerGroupApiAsync(string symbol, CancellationToken ct = default);
    Task<Company> GetCompanyApiAsync(string symbol, CancellationToken ct = default);
}
