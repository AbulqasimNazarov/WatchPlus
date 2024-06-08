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

        public async Task<Film> GetFilmAsync(Guid id)
        {
            var film = await filmRepository.GetByIdAsync(id);
            return film; 
        }
    }
}
