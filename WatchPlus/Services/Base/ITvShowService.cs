using WatchPlus.Models;

namespace WatchPlus.Services.Base;

public interface ITvShowService
{
    public Task CreateNewTvShowAsync(TvSHow newTvShow);
    public Task<TvSHow> GetTvShowAsync(int id);
}
