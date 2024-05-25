namespace WatchPlus.Repositories.Base;

public interface ICreatableAsync<TEntity>
{
    Task CreatableAsync(TEntity obj);
}
