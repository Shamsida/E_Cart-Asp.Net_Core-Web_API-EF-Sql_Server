using E_cart.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_cart.Repository.Interface
{
    public interface IWishlistService
    {
        Task<WishList> AddToWishList(int userId, [FromBody] int prodID);
    }
}
