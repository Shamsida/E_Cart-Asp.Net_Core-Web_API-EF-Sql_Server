using E_cart.Repository;
using E_cart.Repository.Interface;
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
        public async Task<IActionResult> AddToWishList(int userId, [FromBody] int prodID)
        {
            var user = await wishlistService.AddToWishList(userId, prodID);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
    }
}
