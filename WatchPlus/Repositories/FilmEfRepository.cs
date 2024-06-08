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

            if(filmToDelete != null)
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
    }
}
