using BookStoreApi.Models;

namespace TradingView.DAL.Contracts;

public interface IBookRepository
{
    public Task<List<Book>> GetAsync();
}
