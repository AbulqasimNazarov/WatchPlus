using Microsoft.AspNetCore.Mvc;

namespace WatchPlus.Repositories.Base;

public interface ICreatableAsync<TEntity>
{
    public Task CreatableAsync(TEntity obj, IFormFile image);
}
