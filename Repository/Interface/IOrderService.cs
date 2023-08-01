using E_cart.Models;

namespace E_cart.Repository.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> UserOrders(int userId);

        Task<IEnumerable<Order>> GetOrders();
    }
}
