using E_cart.Data;
using E_cart.DTO.ProductDto;
using E_cart.DTO.UserDto;
using E_cart.DTO.WishListDto;
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
        public async Task<WishList> AddToWishList(int userId, int prodID)
        {
            try
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

                var product = await _dataContext.Products.FirstOrDefaultAsync(a => a.Id == prodID);

                if (user == null || product == null)
                {
                    throw new Exception("Invalid user or product ID.");
                }

                var existingItem = await _dataContext.wishlist
                            .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == prodID);

                if (existingItem != null)
                {
                    throw new Exception("The item is already in the wishlist.");
                }
                var wishListItem = new WishList
                  {
                      UserId = userId,
                      ProductId = prodID,
                      Product = product
                  };
                  _dataContext.wishlist.Add(wishListItem);
                  await _dataContext.SaveChangesAsync();

                return wishListItem;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<WishlistDTO>> UserWishlist(int userId)
        {
            try
            {
                var user = await _dataContext.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                var userWishlist = await _dataContext.wishlist
                            .Include(x => x.Product)
                            .Where(a => a.UserId == userId)
                            .ToListAsync();

                var wishlistdto = userWishlist.Select(c => new WishlistDTO
                {
                    Id = c.Id,
                    UserId = userId,
                    Product = new ProductDTO
                    {
                        Id=c.Product.Id,
                        Title=c.Product.Title,
                        Image=c.Product.Image,
                        Price=c.Product.Price,
                        Description=c.Product.Description
                    }    
                });

                return wishlistdto;
            }
            catch (Exception ex)
            {
                throw;
                return null;
            }
        }

        public async Task<IEnumerable<WishList>> GetWishlist()
        {
            try
            {
                var wishlists = await _dataContext.wishlist
                            .Include(x => x.Product)
                            .ToListAsync();

                if (wishlists.Count == 0)
                {
                    throw new Exception("No data found");
                }
                return wishlists;
            }
            catch (Exception ex)
            {
                throw;
                return null;
            }
        }

        public async Task<bool> RemoveFromWishlist(int userId, int productId)
        {
            try
            {
                var wishlistItem = await _dataContext.wishlist
                    .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);

                if (wishlistItem == null)
                {
                    throw new Exception ("Wishlist item not found.");
                }

                _dataContext.wishlist.Remove(wishlistItem);
                await _dataContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }
        }
    }
}
