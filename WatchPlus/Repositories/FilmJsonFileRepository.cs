#pragma warning disable CS1998

using System.Text.Json;
using WatchPlus.Models;
using WatchPlus.Repositories.Base;

public class FilmJsonFileRepository : IFilmRepository
{
    private const string FilmsFilePath = "./Files/films.json";
    public async Task CreatableAsync(Film newFilm)
    {
        var filmJson = System.IO.File.ReadAllText(FilmsFilePath);

        var films = JsonSerializer.Deserialize<List<Film>>(filmJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
        if(films != null){

            if(newFilm.Id == 0){
                newFilm.Id = films.Count + 1;
            }
        }

        films?.Add(newFilm);
        if (films == null)
        {
            throw new Exception("error");
        }

        var resultFilmJson = JsonSerializer.Serialize<List<Film>>(films, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });

        System.IO.File.WriteAllText(FilmsFilePath, resultFilmJson);
    }



    public async Task<IEnumerable<Film>?> GetAllAsync()
    {
        var result = System.IO.File.ReadAllText(FilmsFilePath);
        return JsonSerializer.Deserialize<IEnumerable<Film>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }
}