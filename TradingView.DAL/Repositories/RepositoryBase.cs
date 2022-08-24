using TradingView.DAL.Contracts;

namespace TradingView.DAL.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    public Task AddCollectionAsync(IEnumerable<T> collection)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
