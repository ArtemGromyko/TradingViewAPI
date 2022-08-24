namespace TradingView.DAL.Contracts;

public interface IRepositoryBase<T>
{
    Task<List<T>> GetAllAsync();
    Task AddCollectionAsync(IEnumerable<T> collection);
}
