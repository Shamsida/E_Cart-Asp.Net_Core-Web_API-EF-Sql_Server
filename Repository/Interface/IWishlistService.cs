using E_cart.DTO.WishListDto;
using E_cart.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_cart.Repository.Interface
{
    public interface IWishlistService
    {
        Task<WishList> AddToWishList(int userId, int prodID);

        Task<IEnumerable<WishlistDTO>> UserWishlist(int userId);

        Task<IEnumerable<WishList>> GetWishlist();

        Task<bool> RemoveFromWishlist(int userId, int productId);
    }
}
