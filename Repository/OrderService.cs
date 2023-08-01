using E_cart.Data;
using E_cart.Models;
using E_cart.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_cart.Repository
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;

        public OrderService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<Order>> UserOrders(int userId)
        {
            try
            {
                var user = await _dataContext.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                var orders = await _dataContext.Orders
                            .Include(x => x.OrderDetail)
                            .ThenInclude(x => x.Product)
                            .Where(a => a.UserId == userId)
                            .ToListAsync();
                return orders;
            }
            catch (Exception ex)
            {
                throw;
                return null;
            }
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            try
            {
                var orders = await _dataContext.Orders
                            .Include(x => x.OrderDetail)
                            .ThenInclude(x => x.Product)
                            .ToListAsync();

                if (orders.Count == 0)
                {
                    throw new Exception("No data found");
                }
                return orders;
            }
            catch (Exception ex)
            {
                throw;
                return null;
            }
        }
    }
}
