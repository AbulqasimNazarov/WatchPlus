namespace WatchPlus.Data;

using Microsoft.EntityFrameworkCore;
using WatchPlus.Models;


public class WatchPlusDbContext : DbContext
{
    public DbSet<Film> Films { get; set; }
    public DbSet<TvSHow> TvShows { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Comment> Comments { get; set; }


    public WatchPlusDbContext(DbContextOptions<WatchPlusDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>()
                .HasOne(c => c.Film)
                .WithMany(f => f.Comments)
                .HasForeignKey(c => c.FilmId)
                .OnDelete(DeleteBehavior.Cascade);

        
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Rating>()
            .HasOne(r => r.Film)
            .WithMany(f => f.Ratings)
            .HasForeignKey(r => r.FilmId);

        modelBuilder.Entity<Rating>()
            .HasOne(r => r.User)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.UserId);
    }


}
