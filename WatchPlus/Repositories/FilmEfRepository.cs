using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WatchPlus.Data;
using WatchPlus.Models;
using WatchPlus.Repositories.Base;

namespace WatchPlus.Repositories
{
    public class FilmEfRepository : IFilmRepository
    {
        private readonly WatchPlusDbContext dbContext;


        public FilmEfRepository(WatchPlusDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public async Task CreatableAsync(Film film, IFormFile image)
        {
            film.Id = Guid.NewGuid();

            var extension = new FileInfo(image.FileName).Extension[1..];
            film.Image = $"Assets/FilmsImg/{film.Id}.{extension}";

            using var newFileStream = System.IO.File.Create(film.Image);
            await image.CopyToAsync(newFileStream);

            await dbContext.Films.AddAsync(film);
            await dbContext.SaveChangesAsync();
        }

        public void DeleteById(Guid id)
        {
            var filmToDelete = this.dbContext.Films.FirstOrDefault(f => f.Id == id);

            if (filmToDelete != null)
            {
                this.dbContext.Remove(filmToDelete);
                this.dbContext.SaveChanges();
            }
        }




        public async Task<IEnumerable<Film>?> GetAllAsync()
        {

            return await dbContext.Films.ToListAsync();
        }




        public async Task<Film> GetByIdAsync(Guid id)
        {
            var films = await this.GetAllAsync();
            var film = films?.FirstOrDefault(f => f.Id == id);
            return film ?? throw new Exception("Not Found");
        }


        public async Task<IEnumerable<Film>?> GetFilmsByNameAsync(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            return await dbContext.Films
                .Where(film => film.Name.Contains(name))
                .ToListAsync();
        }


        public async Task<Film?> GetFilmWithHighestRateAsync()
        {
            return await dbContext.Films
                .OrderByDescending(film => film.Rate)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateableAsync(Film film)
        {
            dbContext.Films.Update(film);
            await dbContext.SaveChangesAsync();
        }


        public async Task AddRatingAsync(Guid filmId, int ratingValue, Guid userId)
        {
            var rating = new Rating
            {
                Id = Guid.NewGuid(),
                FilmId = filmId,
                Value = ratingValue,
                UserId = userId
            };

            await dbContext.Ratings.AddAsync(rating);
            await dbContext.SaveChangesAsync();

            await UpdateFilmAverageRatingAsync(filmId);
        }


        public async Task<bool> HasUserRatedFilmAsync(Guid filmId, Guid userId)
        {
            return await dbContext.Ratings.AnyAsync(r => r.FilmId == filmId && r.UserId == userId);
        }

        public async Task UpdateFilmAverageRatingAsync(Guid filmId)
        {
            var film = await dbContext.Films.Include(f => f.Ratings).FirstOrDefaultAsync(f => f.Id == filmId);
            if (film == null)
            {
                throw new Exception("Film not found");
            }

            film.Rate = film.Ratings.Average(r => r.Value);
            dbContext.Films.Update(film);
            await dbContext.SaveChangesAsync();
        }


        public async Task AddCommentAsync(Guid filmId, string text, Guid userId)
        {
            var film = await dbContext.Films.FindAsync(filmId);
            if (film == null)
            {
                throw new Exception("Film not found");
            }
            var users = await dbContext.Users.ToListAsync();
            var user = users?.FirstOrDefault(u => u.Id == userId);
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                FilmId = filmId,
                UserId = userId,
                UserImage = user.Image,
                UserName = user.Name,
                Text = text,
                CreatedDate = DateTime.UtcNow
            };

            dbContext.Comments.Add(comment);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsById(Guid filmId)
        {
            return await dbContext.Comments
                                    .Where(comment => comment.FilmId == filmId)
                          .OrderByDescending(comment => comment.CreatedDate)
                          .Select(comment => new Comment
                          {
                              Id = comment.Id,
                              FilmId = comment.FilmId,
                              UserId = comment.UserId,
                              UserName = comment.User.Name,  // Assuming User is a navigation property
                              UserImage = comment.User.Image, // Assuming User is a navigation property
                              Text = comment.Text,
                              CreatedDate = comment.CreatedDate
                          })
                          .ToListAsync();
        }

    }
}
