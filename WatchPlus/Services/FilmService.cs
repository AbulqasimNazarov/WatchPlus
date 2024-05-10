using System.Linq.Expressions;
using WatchPlus.Models;
using WatchPlus.Repositories.Base;
using WatchPlus.Services.Base;

namespace WatchPlus.Services;

public class FilmService : IFilmService
{
    private readonly IFilmRepository filmRepository;
    public FilmService(IFilmRepository filmRepository)
    {
        this.filmRepository = filmRepository;
    }

    public async Task CreateNewFilmAsync(Film newFilm)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(newFilm.Name);

        await this.filmRepository.CreatableAsync(newFilm);
    }

    public async Task<Film> GetFilmAsync(int id)
    {
        
        var films = await this.filmRepository.GetAllAsync();
        if (films != null)
        {
            var film = films.FirstOrDefault(f => f.Id == id);
            if (film != null)
            {
                return film;

            }
        }

        throw new Exception("Not Found");
    }
}
