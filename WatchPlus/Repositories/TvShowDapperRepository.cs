using System.Data.SqlClient;
using ConfigurationApp.Options.Connections;
using Dapper;
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
    public async Task CreatableAsync(TvSHow film)
    {
        using var connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync(
            @"insert into TVShows(Name, Description, Category, Star, Rate, Image, VideoTrailer) 
            values (@Name, @Description, @Category, @Star, @Rate, @Image, @VideoTrailer)", film);
    }

    public async Task<IEnumerable<TvSHow>?> GetAllAsync()
    {
        using var connection = new SqlConnection(connectionString);

        return await connection.QueryAsync<TvSHow>(@"select * from TVShows");
    }

}
