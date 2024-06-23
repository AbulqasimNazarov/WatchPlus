using System.Security.Claims;
using WatchPlus.Models;
using WatchPlus.Repositories.Base;
using WatchPlus.Services.Base;

namespace WatchPlus.Services
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository filmRepository;


        public FilmService(IFilmRepository filmRepository)
        {
            this.filmRepository = filmRepository;

        }

        public async Task CreateNewFilmAsync(Film newFilm, IFormFile image)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(newFilm.Name);
            await filmRepository.CreatableAsync(newFilm, image);
        }

        public void DeleteFilmById(Guid id)
        {
            filmRepository.DeleteById(id);
        }


        public async Task<IEnumerable<Film>> GetAllFilmsAsync()
        {
            var films = await filmRepository.GetAllAsync();

            return films ?? Enumerable.Empty<Film>();
        }

        public async Task<IEnumerable<Film>> GetAllFilmsbyNameAsync(string? name)
        {
            var films = await filmRepository.GetFilmsByNameAsync(name);

            return films ?? Enumerable.Empty<Film>();
        }


        public async Task<Film> GetFilmAsync(Guid id)
        {
            var film = await filmRepository.GetByIdAsync(id);
            return film;
        }

        public async Task<Film> GetFilmWithHighestRateAsync()
        {
            var film = await filmRepository.GetFilmWithHighestRateAsync();
            return film;
        }

        public async Task UpdateFilmAsync(Film film)
        {
            await filmRepository.UpdateableAsync(film);
        }


        public async Task<double> GetAverageRatingAsync(Guid filmId)
        {
            var film = await filmRepository.GetByIdAsync(filmId);
            if (film == null)
            {
                throw new Exception("Film not found");
            }
            return film.Rate;
        }

        public async Task AddRatingAsync(Guid filmId, int ratingValue, Guid userId)
        {
            await filmRepository.AddRatingAsync(filmId, ratingValue, userId);
        }

        public async Task<bool> HasUserRatedFilmAsync(Guid filmId, Guid userId)
        {
            return await filmRepository.HasUserRatedFilmAsync(filmId, userId);
        }

        public async Task AddCommentAsync(Guid filmId, string text, Guid userId)
        {
            await filmRepository.AddCommentAsync(filmId, text, userId);
        }

        public async Task<IEnumerable<Comment>> GetFilmCommentsBYIdAsync(Guid filmId)
        {
            return await filmRepository.GetCommentsById(filmId);
        }


    }
}
