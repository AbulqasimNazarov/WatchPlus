using System.Text.Json;
using WatchPlus.Models;
using WatchPlus.Repositories.Base;

public class JsonRepository : IFilmRepository
{
    public IEnumerable<Film>? GetAll(string path)
    {

        var result = System.IO.File.ReadAllText(path);
        return JsonSerializer.Deserialize<IEnumerable<Film>>(result, new JsonSerializerOptions{
            PropertyNameCaseInsensitive = true,
        });
    }

    
}