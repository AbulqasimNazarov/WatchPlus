using WatchPlus.Models;

namespace WatchPlus.Services.Base;

public interface ITvShowService
{
    public Task CreateNewTvShowAsync(TvSHow newTvShow, IFormFile image);
    public Task<TvSHow> GetTvShowAsync(Guid id);
    public Task<IEnumerable<TvSHow>> GetAllTvShowsAsync();
    public void DeleteTvShowById(Guid id);
}
