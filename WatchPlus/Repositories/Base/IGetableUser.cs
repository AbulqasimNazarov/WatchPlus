namespace WatchPlus.Repositories.Base;

public interface IGetableUser<TEntity>
{
    public Task<IEnumerable<TEntity>?> GetAllAsync();
    public Task<TEntity> GetByEmailAsync(string? email);
    public Task<TEntity> GetByIdAsync(Guid id);
}
