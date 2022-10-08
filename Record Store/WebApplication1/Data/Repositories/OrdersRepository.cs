using Microsoft.EntityFrameworkCore;
using Record_Store.Data.DTOS.Orders;
using Record_Store.Entity;
using Record_Store.Helpers;

namespace Record_Store.Data.Repositories
{
    public interface IOrdersRepository
    {
        Task<Order?> GetOrder(uint orderID);
        Task<IReadOnlyList<Order>> GetOrdersManyAsync();
        Task<PageList<Order>> GetOrdersManyPagedAsync(SearchParameters orderSearchParameters);
        Task CreateOrder(Order order);
        Task UpdateOrder(Order order);
        Task RemoveOrder(Order order);

    }
    public class OrdersRepository : IOrdersRepository
    {
        private readonly RsDbContext _rsDbContext;

        public OrdersRepository(RsDbContext rsDbContext)
        {
            _rsDbContext=rsDbContext;
        }

        public async Task<Order?> GetOrder(uint orderID)
        {
            return await _rsDbContext.Orders.FirstOrDefaultAsync(o => o.ID == orderID);
        }


        public async Task<IReadOnlyList<Order>> GetOrdersManyAsync()
        {
            return await _rsDbContext.Orders.ToListAsync();
        }

        public async Task<PageList<Order>> GetOrdersManyPagedAsync(SearchParameters orderSearchParameters)
        {
            var quaryable = _rsDbContext.Orders.AsQueryable().OrderBy(o => o.ID);
            return await PageList<Order>.CreateAsync(quaryable, orderSearchParameters.PageNumber, orderSearchParameters.PageSize);
        }

        public async Task CreateOrder(Order order)
        {
            _rsDbContext.Orders.Add(order);
            await _rsDbContext.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            _rsDbContext.Orders.Update(order);
            await _rsDbContext.SaveChangesAsync();
        }

        public async Task RemoveOrder(Order order)
        {
            _rsDbContext.Orders.Remove(order);
            await _rsDbContext.SaveChangesAsync();
        }
    }
}
