using E_cart.Repository;
using E_cart.Repository.Interface;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            this.wishlistService = wishlistService;
        }

        [HttpPost("AddToWishList")]
        public async Task<IActionResult> AddToWishList(int userId, int prodID)
        {
            var user = await wishlistService.AddToWishList(userId, prodID);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        [HttpGet("GetWishlistById")]
        public async Task<IActionResult> UserWishlist(int userId)
        {
            var itm = await wishlistService.UserWishlist(userId);
            if (itm == null)
            {
                return NotFound();
            }
            return Ok(itm);
        }

        [HttpGet("GetWishlists")]
        public async Task<IActionResult> GetWishlist()
        {
            var itm = await wishlistService.GetWishlist();
            if (itm == null)
            {
                return NotFound();
            }
            return Ok(itm);
        }

        [HttpDelete("DeleteWishlistItem")]
        public async Task<IActionResult> RemoveFromWishlist(int userId, int productId)
        {
            try
            {
                var itm = await wishlistService.RemoveFromWishlist(userId, productId);
                if (!itm)
                {
                    return BadRequest("Error");
                }
                return Ok("Removed Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
