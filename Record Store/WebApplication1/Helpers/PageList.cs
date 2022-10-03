using Microsoft.EntityFrameworkCore;

namespace Record_Store.Helpers
{
    public class PageList<T> : List<T>
    {
        public uint CurrentPage { get; private set; }
        public uint TotalPages { get; private set; }
        public uint PageSize { get; private set; }
        public uint TotalCount { get; private set; }

        public bool hasPrevious => (CurrentPage > 1);
        public bool hasNext => (CurrentPage < TotalPages);

        public PageList(List<T> items, uint count, uint pageNumber, uint pageSize)
        {
            TotalCount=count;
            PageSize=pageSize;
            CurrentPage=pageNumber;
            TotalPages=(uint)Math.Ceiling(count/(double)pageSize);
            AddRange(items);
        }
        public static async Task<PageList<T>> CreateAsync(IQueryable<T> source, uint pagenumber, uint pageSize)
        {
            var count = source.Count();
            var items = await source.Skip((int)((pagenumber -1) *pageSize)).Take((int)pageSize).ToListAsync();
            return new PageList<T>(items, (uint)count, pagenumber, pageSize);
        }
    }
}