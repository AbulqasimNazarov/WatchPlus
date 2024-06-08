namespace WatchPlus.Services.Base;

using Microsoft.AspNetCore.Mvc;

using WatchPlus.Models;
public interface IFilmService
{
    public Task CreateNewFilmAsync(Film newFilm, IFormFile image);
    public Task<Film> GetFilmAsync(Guid id);
    public Task<IEnumerable<Film>> GetAllFilmsAsync();

    public void DeleteFilmById(Guid id);
}
