namespace WatchPlus.Repositories.Base;

public interface IUpdateUser<TEntity>
{
    public Task UpdateUserRoleAsync(TEntity user);
    public Task UpdateUserAsync(TEntity user);
}
