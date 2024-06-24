#pragma warning disable CS8618

using WatchPlus.Connections.Base;

namespace ConfigurationApp.Options.Connections;

public class MsSqlConnectionOptions : IConnectionOptions
{
    public string Database { get; set; }
    public string Server { get; set; }
    public string UserId { get; set; }
    public string Password { get; set; }
    public string? TrustServerCertificate { get; set; }

    public string ConnectionString
    {
        get
        {
            ArgumentException.ThrowIfNullOrEmpty(Database);
            ArgumentException.ThrowIfNullOrEmpty(Server);
            

            var connectionString = $"Server={this.Server};Database={this.Database};";

            if (string.IsNullOrWhiteSpace(this.TrustServerCertificate) == false)
            {
                connectionString += $"Trusted_Connection={this.TrustServerCertificate};";
            }

            return connectionString;
        }
    }
}