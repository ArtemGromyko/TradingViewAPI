using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.DAL.Contracts.ApiServices;
public interface IStockProfileApiService
{
    Task<CEOCompensation> GetCEOCompensationApiAsync(CancellationToken ct = default);
    Task<InsiderRoster> GetInsiderRosterApiAsync(CancellationToken ct = default);
    Task<List<InsiderSummaryItem>> GetInsiderSummaryApiAsync(CancellationToken ct = default);
    Task<List<InsiderTransactionsItem>> GetInsiderTransactionsApiAsync(CancellationToken ct = default);
    Task<Logo> GetLogoApiAsync(CancellationToken ct = default);
    Task<PeerGroup> GetPeerGroupApiAsync(CancellationToken ct = default);
    Task<Company> GetCompanyApiAsync(CancellationToken ct = default);
}
