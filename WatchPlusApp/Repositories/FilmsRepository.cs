namespace WatchPlusApp.Repositories;

using System.Data.SqlClient;
using Dapper;
using WatchPlusApp.Models;


public class FilmsRepository
{
    private readonly SqlConnection sqlConnection;
    private const string connectionString = $"Server=localhost;Database=FilmsDb;Trusted_Connection=True;";
    public FilmsRepository()
    {
        this.sqlConnection = new SqlConnection(connectionString);
        this.sqlConnection.Open();
    }

    public IEnumerable<Films> GetAll()
    {
        return this.sqlConnection.Query<Films>(sql: @$"select * from Films");
    }

    public void Add(Films film)
    {
        this.sqlConnection.Execute("INSERT INTO Films (Name, Rate) VALUES (@Name, @Rate)", film);
    }
}

