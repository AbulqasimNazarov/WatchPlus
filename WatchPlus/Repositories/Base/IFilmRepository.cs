using WatchPlus.Models;

namespace WatchPlus.Repositories.Base;

public interface IFilmRepository : IGetableAsync<Film>, ICreatableAsync<Film>, IDeletable
{
    
    
}
