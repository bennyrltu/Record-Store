using Microsoft.EntityFrameworkCore;
using Record_Store.Data.DTOS.Orders;
using Record_Store.Entity;
using Record_Store.Helpers;

namespace Record_Store.Data.Repositories
{
    public interface IRecordsRepository
    {
        Task<Recording?> GetRecording(uint orderID, uint recordingID);
        Task<IReadOnlyList<Recording>> GetRecordingsManyAsync(uint orderID);
        Task<PageList<Recording>> GetRecordingsManyPagedAsync(SearchParameters recordSearchParameters);
        Task CreateRecording(Recording recording);
        Task UpdateRecording(Recording recording);
        Task RemoveRecording(Recording recording);

    }

    public class RecordsRepository : IRecordsRepository
    {
        private readonly RsDbContext _rsDbContext;

        public RecordsRepository(RsDbContext rsDbContext)
        {
            _rsDbContext=rsDbContext;
        }

        public async Task<Recording?> GetRecording(uint orderID, uint recordingID)
        {
            return await _rsDbContext.Recordings.FirstOrDefaultAsync( o=> o.OrderId == orderID && o.ID == recordingID);
        }


        public async Task<IReadOnlyList<Recording>> GetRecordingsManyAsync(uint orderID)
        {
            return await _rsDbContext.Recordings.Where(o => o.OrderId == orderID).ToListAsync();
        }

        public async Task<PageList<Recording>> GetRecordingsManyPagedAsync(SearchParameters orderSearchParameters)
        {
            var quaryable = _rsDbContext.Recordings.AsQueryable().OrderBy(o => o.ID);
            return await PageList<Recording>.CreateAsync(quaryable, orderSearchParameters.PageNumber, orderSearchParameters.PageSize);
        }

        public async Task CreateRecording(Recording recording)
        {
            _rsDbContext.Recordings.Add(recording);
            await _rsDbContext.SaveChangesAsync();
        }

        public async Task UpdateRecording(Recording recording)
        {
            _rsDbContext.Recordings.Update(recording);
            await _rsDbContext.SaveChangesAsync();
        }

        public async Task RemoveRecording(Recording recording)
        {
            _rsDbContext.Recordings.Remove(recording);
            await _rsDbContext.SaveChangesAsync();
        }
    }
}