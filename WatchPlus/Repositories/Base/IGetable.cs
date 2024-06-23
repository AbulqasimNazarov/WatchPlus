using WatchPlus.Models;

namespace WatchPlus.Repositories.Base;

public interface IGetableAsync<TEntity>
{
    public Task<IEnumerable<TEntity>?> GetAllAsync();
    public Task<IEnumerable<TEntity>?> GetFilmsByNameAsync(string? name);
    public Task<TEntity> GetByIdAsync(Guid id);
    public Task<TEntity> GetFilmWithHighestRateAsync();
    public Task<bool> HasUserRatedFilmAsync(Guid filmId, Guid userId);
    public Task<IEnumerable<Comment>> GetCommentsById(Guid filmId);
    
}
