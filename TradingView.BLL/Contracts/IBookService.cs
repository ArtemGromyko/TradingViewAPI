using BookStoreApi.Models;

namespace TradingView.BLL.Contracts;

public interface IBookService
{
    public Task<List<Book>> GetAsync();
}
