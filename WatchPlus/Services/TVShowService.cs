#pragma warning disable CS8603


using WatchPlus.Models;
using WatchPlus.Repositories.Base;
using WatchPlus.Services.Base;

namespace WatchPlus.Services;

public class TVShowService : ITvShowService
{
    private readonly ITVShowRepository tvShowRepository;
    public TVShowService(ITVShowRepository tvShowRepository)
    {
        this.tvShowRepository = tvShowRepository;
    }
    public async Task CreateNewTvShowAsync(TvSHow newTvShow)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(newTvShow.Name);

        await this.tvShowRepository.CreatableAsync(newTvShow);
    }


    

    
    public async Task<TvSHow> GetTvShowAsync(int id)
    {
        var tvShows = await this.tvShowRepository.GetAllAsync();
        if (tvShows != null)
        {
            var tvShow = tvShows.FirstOrDefault(f => f.Id == id);
            if (tvShow != null)
            {
                return tvShow;

            }
        }

        throw new Exception("Not Found");
    }

    public async Task<IEnumerable<TvSHow>> GetTvShowsAsync()
    {
        return await this.tvShowRepository.GetAllAsync();
    }
}
