namespace WatchPlus.Services.Base;

using Microsoft.AspNetCore.Mvc;

using WatchPlus.Models;
public interface IFilmService
{
    public Task CreateNewFilmAsync(Film newFilm, IFormFile image);
    public Task<Film> GetFilmAsync(Guid id);
    
    public Task<Film> GetFilmWithHighestRateAsync();
    public Task<IEnumerable<Film>> GetAllFilmsAsync();
    public Task<IEnumerable<Film>> GetAllFilmsbyNameAsync(string? name);
    public void DeleteFilmById(Guid id);

    public Task UpdateFilmAsync(Film film);
    public Task<double> GetAverageRatingAsync(Guid filmId);
    
    public Task AddRatingAsync(Guid filmId, int ratingValue, Guid userId);
    public Task<bool> HasUserRatedFilmAsync(Guid filmId, Guid userId);
    public Task AddCommentAsync(Guid filmId, string text, Guid userId);
    public Task<IEnumerable<Comment>> GetFilmCommentsBYIdAsync(Guid filmId);
}
