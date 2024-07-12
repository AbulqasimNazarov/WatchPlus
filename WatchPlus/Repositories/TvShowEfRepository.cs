using Microsoft.EntityFrameworkCore;
using WatchPlus.Data;
using WatchPlus.Models;
using WatchPlus.Repositories.Base;

namespace WatchPlus.Repositories
{
    public class TvShowEfRepository : ITVShowRepository
    {
        private readonly WatchPlusDbContext dbContext;

        public TvShowEfRepository(WatchPlusDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreatableAsync(TvSHow tvShow, IFormFile image)
        {
            tvShow.Id = Guid.NewGuid();
            var extension = new FileInfo(image.FileName).Extension[1..];
            tvShow.Image = $"Assets/TvShowImg/{tvShow.Id}.{extension}";

            using var newFileStream = System.IO.File.Create(tvShow.Image);
            await image.CopyToAsync(newFileStream);

            await dbContext.TvShows.AddAsync(tvShow);
            await dbContext.SaveChangesAsync();
        }

        public void DeleteById(Guid id)
        {
            var tvShowToDelete = this.dbContext.TvShows.FirstOrDefault(f => f.Id == id);

            if(tvShowToDelete != null)
            {
                this.dbContext.Remove(tvShowToDelete);
                this.dbContext.SaveChanges();
            }
        }

        public async Task<IEnumerable<TvSHow>?> GetAllAsync()
        {
            return await dbContext.TvShows.ToListAsync();
        }

       

        public async Task<TvSHow> GetByIdAsync(Guid id)
        {
            var tvShows = await this.GetAllAsync();
            var tvShow = tvShows?.FirstOrDefault(f => f.Id == id);
            return tvShow ?? throw new Exception("Not Found");
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
}
