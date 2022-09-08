namespace TradingView.BLL.Contracts.RealTime;

public interface IPriceOnlyService
{
    Task<double> GetPriceOnlyAsync(string symbol);
}
