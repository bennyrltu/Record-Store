using Microsoft.EntityFrameworkCore;
using Record_Store.Data.DTOS.Orders;
using Record_Store.Entity;
using Record_Store.Helpers;


namespace Record_Store.Data.Repositories
{
    public interface IRatingsRepository
    {
        Task<Rating?> GetRating(uint recordingID, uint ratingID);
        Task<IReadOnlyList<Rating>> GetRatingsManyAsync(uint recordingID);
        Task<PageList<Rating>> GetRatingsManyPagedAsync(SearchParameters orderSearchParameters);
        Task CreateReating(Rating rating);
        Task UpdateRating(Rating rating);
        Task RemoveRating(Rating rating);

    }
    public class RatingsRepository : IRatingsRepository
    {
        private readonly RsDbContext _rsDbContext;

        public RatingsRepository(RsDbContext rsDbContext)
        {
            _rsDbContext=rsDbContext;
        }

        public async Task<Rating?> GetRating(uint recordingID, uint ratingID)
        {
            return await _rsDbContext.Ratings.FirstOrDefaultAsync( o => o.RecordingID == recordingID && o.ID == ratingID);
        }


        public async Task<IReadOnlyList<Rating>> GetRatingsManyAsync(uint recordingID)
        {
            return await _rsDbContext.Ratings.Where(o => o.RecordingID == recordingID).ToListAsync();
        }

        public async Task<PageList<Rating>> GetRatingsManyPagedAsync(SearchParameters orderSearchParameters)
        {
            var quaryable = _rsDbContext.Ratings.AsQueryable().OrderBy(o => o.ID);
            return await PageList<Rating>.CreateAsync(quaryable, orderSearchParameters.PageNumber, orderSearchParameters.PageSize);
        }

        public async Task CreateReating(Rating rating)
        {
            _rsDbContext.Ratings.Add(rating);
            await _rsDbContext.SaveChangesAsync();
        }

        public async Task UpdateRating(Rating rating)
        {
            _rsDbContext.Ratings.Update(rating);
            await _rsDbContext.SaveChangesAsync();
        }

        public async Task RemoveRating(Rating rating)
        {
            _rsDbContext.Ratings.Remove(rating);
            await _rsDbContext.SaveChangesAsync();
        }
    }
}