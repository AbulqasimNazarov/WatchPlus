namespace WatchPlus.Repositories.Base
{
    public interface IUpdateableAsync<TEntity>
    {
        public Task UpdateableAsync(TEntity obj);
        public Task AddRatingAsync(Guid filmId, int ratingValue, Guid userId);
        public Task UpdateFilmAverageRatingAsync(Guid filmId);
        public Task AddCommentAsync(Guid filmId, string text, Guid userId);
        
    }
}