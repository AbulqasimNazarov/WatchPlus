namespace WatchPlus.Repositories.Base;

public interface IGetableAsync<TEntity>
{
    public Task<IEnumerable<TEntity>?> GetAllAsync();
    public Task<TEntity> GetByIdAsync(Guid id);
}
