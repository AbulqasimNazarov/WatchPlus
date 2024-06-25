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


        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Film>().HasData(
            new Film
            {
                Id = new Guid("f8a8c851-2cbc-48c2-a439-bdc494d6329a"),
                Name = "Need for Speed",
                Presentation = "Fresh from prison, a street racer who was framed by a wealthy business associate joins a cross-country race with revenge in mind. His ex-partner, learning of the plan, places a massive bounty on his head as the race begins.",
                Category = new List<string> { "Action", "Crime", "Thriller" },
                Star = "Aaron Paul",
                TrailerVideo = "https://www.youtube.com/embed/u3wtVI-aJuw",
                Image = "Assets/FilmsImg/f8a8c851-2cbc-48c2-a439-bdc494d6329a.jpg",
                Rate = 4.5,
                PresentationDate = DateTime.Parse("2014-03-13")
            },
            new Film
            {
                Id = new Guid("ba466485-1828-4a68-8976-d7d5ca5a7862"),
                Name = "The Gentlemen",
                Presentation = "An American expat tries to sell off his highly profitable marijuana empire in London, triggering plots, schemes, bribery and blackmail in an attempt to steal his domain out from under him.",
                Category = new List<string> { "Action", "Crime" },
                Star = "Matthew McConaughey",
                TrailerVideo = "https://www.youtube.com/embed/Ify9S7hj480",
                Image = "Assets/FilmsImg/ba466485-1828-4a68-8976-d7d5ca5a7862.jpg",
                Rate = 4.7,
                PresentationDate = DateTime.Parse("2020-01-24")
            },
            new Film
            {
                Id = new Guid("7cea2648-a8fc-4cca-9730-85bf83ac437c"),
                Name = "Teenage Mutant Ninja Turtles",
                Presentation = "When a kingpin threatens New York City, a group of mutated turtle warriors must emerge from the shadows to protect their home.",
                Category = new List<string> { "Action", "Adventure", "Comedy" },
                Star = "Megan Fox",
                TrailerVideo = "https://www.youtube.com/embed/VZZ0PnDZdZk",
                Image = "Assets/FilmsImg/7cea2648-a8fc-4cca-9730-85bf83ac437c.jpg",
                Rate = 4.3,
                PresentationDate = DateTime.Parse("2014-08-08")
            },
            new Film
            {
                Id = new Guid("6a751f98-7906-4cc9-bb48-19cc37947d81"),
                Name = "Kingsman: The Secret Service",
                Presentation = "A spy organisation recruits a promising street kid into the agency's training program, while a global threat emerges from a twisted tech genius.",
                Category = new List<string> { "Action", "Comedy" },
                Star = "Colin Firth",
                TrailerVideo = "https://www.youtube.com/embed/kl8F-8tR8to",
                Image = "Assets/FilmsImg/6a751f98-7906-4cc9-bb48-19cc37947d81.jpg",
                Rate = 4.4,
                PresentationDate = DateTime.Parse("2015-02-13")
            },
            new Film
            {
                Id = new Guid("27bad295-e53c-4060-8fb5-57681f1e4354"),
                Name = "Spider-man",
                Presentation = "After being bitten by a genetically-modified spider, a shy teenager gains spider-like abilities that he uses to fight injustice as a masked superhero and face a vengeful enemy.",
                Category = new List<string> { "Action", "Adventure", "Sci-Fi" },
                Star = "Tobey Maguire",
                TrailerVideo = "https://www.youtube.com/embed/t06RUxPbp_c",
                Image = "Assets/FIlmsImg/27bad295-e53c-4060-8fb5-57681f1e4354.jpg",
                Rate = 4.6,
                PresentationDate = DateTime.Parse("2002-05-03")
            }
        );

        
    }


}
