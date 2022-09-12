using TradingView.DAL.Entities.RealTime.Book;

namespace TradingView.BLL.Contracts.RealTime;

public interface IBookService
{
    Task<BookItem> GetBookAsync(string symbol);
}
