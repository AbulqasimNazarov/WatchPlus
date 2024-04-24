using System.Text.Json;
using WatchPlus.Models;
using WatchPlus.Repositories.Base;

public class JsonRepository : IFilmRepository
{
    public void CreateFilm(Film newFilm)
    {
        var filmJson = System.IO.File.ReadAllText("./Files/films.json");

        var films = JsonSerializer.Deserialize<List<Film>>(filmJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
        if (films != null)
        {

            if (newFilm.Id == 0)
            {
                newFilm.Id = films.Count + 1;
            }
        }

        films?.Add(newFilm);

        var resultFilmJson = JsonSerializer.Serialize<List<Film>>(films, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });

        System.IO.File.WriteAllText("./Files/films.json", resultFilmJson);
    }

    public IEnumerable<Film>? GetAll(string path)
    {

        var result = System.IO.File.ReadAllText(path);
        return JsonSerializer.Deserialize<IEnumerable<Film>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }


}