using BookStoreApi.Models;
using TradingView.BLL.Contracts;
using TradingView.DAL.Contracts;

namespace TradingView.BLL.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<Book>> GetAsync() => await _bookRepository.GetAsync();
}
