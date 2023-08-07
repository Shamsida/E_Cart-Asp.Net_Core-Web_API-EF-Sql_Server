using E_cart.DTO.CartDto;
using E_cart.DTO.UserDto;
using E_cart.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_cart.Repository.Interface
{
    public interface ICartService
    {
        Task<IEnumerable<CartDataDTO>> GetCart(int userId);

        Task<Cart> AddToCart(int userId, [FromBody] AddtoCartDTO itm);

        Task<bool> RemoveFromCart(int userId, int prodID);

        Task<bool> DoCheckout(int userId);

        Task<bool> IncreaseQuantity(int cartDetailId);
    }
}
