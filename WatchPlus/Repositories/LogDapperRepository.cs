using System.Data.SqlClient;
using ConfigurationApp.Options.Connections;
using Dapper;
using Microsoft.Extensions.Options;
using WatchPlus.Models;
using WatchPlus.Repositories.Base;

namespace WatchPlus.Repositories;

public class LogDapperRepository : ILogRepository
{
    private readonly string connectionString;
    
    public LogDapperRepository(IOptionsSnapshot<MsSqlConnectionOptions> msSqlConnectionOptions)
    {
        this.connectionString = msSqlConnectionOptions.Value.ConnectionString;
    }
    public async Task CreatableAsync(Log log)
    {
        using var connection = new SqlConnection(connectionString);
        await connection.ExecuteAsync(
            @"insert into Logs(Url, RequestBody, ResponseBody, StatusCode, HttpMethod) 
            values (@Url, @RequestBody, @ResponseBody, @StatusCode, @HttpMethod)", log);
        
    }

    public Task<IEnumerable<Log>?> GetAllAsync()
    {
        throw new NotImplementedException();
    }

}
