namespace TradingView.DAL.Contracts.ApiServices;
public interface IStockProfileApiService
{
    Task GetCEOCompensationApiAsync(CancellationToken ct = default);
    Task GetInsiderRosterApiAsync(CancellationToken ct = default);
    Task GetInsiderSummaryApiAsync(CancellationToken ct = default);
    Task GetInsiderTransactionsApiAsync(CancellationToken ct = default);
    Task GetLogoApiAsync(CancellationToken ct = default);
    Task GetPeerGroupApiAsync(CancellationToken ct = default);
    Task GetCompanyApiAsync(CancellationToken ct = default);
}
