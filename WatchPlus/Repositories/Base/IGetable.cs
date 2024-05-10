namespace WatchPlus.Repositories.Base;

public interface IGetableAsync<TEntity>
{
    Task<IEnumerable<TEntity>?> GetAllAsync();
}
