namespace WatchPlus.Data;

using Microsoft.EntityFrameworkCore;
using WatchPlus.Models;


public class WatchPlusDbContext : DbContext
{
    public DbSet<Film> Films { get; set; }
    public DbSet<TvSHow> TvShows { get; set; }
    public DbSet<Log> Logs { get; set; }

    public WatchPlusDbContext(DbContextOptions<WatchPlusDbContext> options) : base(options)
    {
        
    }
}
