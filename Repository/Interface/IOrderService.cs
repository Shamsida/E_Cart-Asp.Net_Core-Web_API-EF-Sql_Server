using E_cart.DTO.CartDto;
using E_cart.DTO.OrderDto;
using E_cart.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_cart.Repository.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> UserOrders(int userId);
        Task<IEnumerable<Order>> UsersOrder(int Id);

        Task<Order> AddToOrder(int userId, [FromBody] OrderCreateDTO itm);

        Task<IEnumerable<Order>> GetOrders();
    }
}
