namespace WatchPlus.Services.Base;


using WatchPlus.Models;
public interface IFilmService
{
    public Task CreateNewFilmAsync(Film newFilm);
    public Task<Film> GetFilmAsync(int id);
}
