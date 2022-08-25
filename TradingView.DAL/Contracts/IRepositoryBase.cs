namespace TradingView.DAL.Contracts;

public interface IRepositoryBase<TEntity>
{
    Task<List<TEntity>> GetAllAsync();
    Task AddCollectionAsync(IEnumerable<TEntity> collection);
}
