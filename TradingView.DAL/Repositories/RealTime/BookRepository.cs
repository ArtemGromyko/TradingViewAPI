using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Entities.RealTime.Book;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories.RealTime;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
    public BookRepository(IOptions<DatabaseSettings> settings, IConfiguration configuration)
        : base(settings, configuration["MongoDBCollectionNames:BookCollectionName"])
    {
    }
}
