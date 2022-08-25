using TradingView.DAL.Entities;

namespace TradingView.BLL.Contracts;

public interface IHistoricalPricesService
{
    Task<List<DividendInfo>> GetAllDividendsAsync();
    Task<List<HistoricalPrice>> GetAllHistoricalPricesAsync();
    Task<List<SymbolInfo>> GetAllSymbolsAsync();
    Task<List<ExchangeInfo>> GetAllExchanges();
}
