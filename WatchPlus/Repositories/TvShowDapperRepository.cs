using System.Data.SqlClient;
using ConfigurationApp.Options.Connections;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WatchPlus.Models;
using WatchPlus.Repositories.Base;

namespace WatchPlus.Repositories;

public class TvShowDapperRepository : ITVShowRepository
{
    private readonly string connectionString;
    public TvShowDapperRepository(IOptionsSnapshot<MsSqlConnectionOptions> msSqlConnectionOptions)
    {
        this.connectionString = msSqlConnectionOptions.Value.ConnectionString;
    }
    public async Task CreatableAsync(TvSHow tvShow, IFormFile image)
    {
        tvShow.Id = Guid.NewGuid();

        using var connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync(
            @"insert into TVShows(Name, Presentation, Category, Star, Rate, Image, TrailerVideo) 
            values (@Name, @Presentation, @Category, @Star, @Rate, @Image, @TrailerVideo)", tvShow);
    }

    public void DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }


    public async Task<IEnumerable<TvSHow>?> GetAllAsync()
    {
        using var connection = new SqlConnection(connectionString);

        return await connection.QueryAsync<TvSHow>(@"select * from TVShows");
    }

    public Task<double> GetAverageRatingAsync(Guid filmId)
    {
        throw new NotImplementedException();
    }


    public Task<TvSHow> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Comment>> GetCommentsById(Guid filmId)
    {
        throw new NotImplementedException();
    }


    public Task<IEnumerable<TvSHow>?> GetFilmsByNameAsync(string? name)
    {
        throw new NotImplementedException();
    }

    public Task<TvSHow> GetFilmWithHighestRateAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasUserRatedFilmAsync(Guid filmId, Guid userId)
    {
        throw new NotImplementedException();
    }

}
