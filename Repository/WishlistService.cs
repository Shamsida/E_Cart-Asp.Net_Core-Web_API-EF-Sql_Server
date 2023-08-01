using E_cart.Data;
using E_cart.Models;
using E_cart.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_cart.Repository
{
    public class WishlistService : IWishlistService
    {
        private readonly DataContext _dataContext;

        public WishlistService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<WishList> AddToWishList(int userId, [FromBody] int prodID)
        {
            try
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

                var product = await _dataContext.Products.FirstOrDefaultAsync(a => a.Id == prodID);

                if (user == null || product == null)
                {
                    throw new Exception("Invalid user or product ID.");
                }

                var existingItem = await _dataContext.WishList
                            .FirstOrDefaultAsync(c => c.User.Id == userId && c.ProductId == prodID);

                if (existingItem != null)
                {
                    throw new Exception("The item is already in the wishlist.");
                }
                var wishListItem = new WishList
                  {
                      User = user,
                      ProductId = prodID,
                      Product = product
                  };
                  _dataContext.WishList.Add(wishListItem);
                  await _dataContext.SaveChangesAsync();

                return wishListItem;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
