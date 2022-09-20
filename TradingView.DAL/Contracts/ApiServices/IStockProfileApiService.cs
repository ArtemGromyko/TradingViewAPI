namespace TradingView.DAL.Contracts.ApiServices;
public interface IStockProfileApiService
{
    Task GetCEOCompensationApiAsync(string symbol, CancellationToken ct = default);
    Task GetInsiderRosterApiAsync(string symbol, CancellationToken ct = default);
    Task GetInsiderSummaryApiAsync(string symbol, CancellationToken ct = default);
    Task GetInsiderTransactionsApiAsync(string symbol, CancellationToken ct = default);
    Task GetLogoApiAsync(string symbol, CancellationToken ct = default);
    Task GetPeerGroupApiAsync(string symbol, CancellationToken ct = default);
    Task GetCompanyApiAsync(string symbol, CancellationToken ct = default);
}
