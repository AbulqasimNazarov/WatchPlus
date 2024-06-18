using WatchPlus.Models;

namespace WatchPlus.Repositories.Base;

public interface IFilmRepository
{
    
    public IEnumerable<Film>? GetAll(string path);
    
    public void CreateFilm(Film newFilm);
    
    
}
