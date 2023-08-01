using E_cart.DTO.UserDto;
using E_cart.Models;
using E_cart.Repository;
using E_cart.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var itm = await cartService.GetCart(userId);
            if (itm == null)
            {
                return NotFound();
            }
            return Ok(itm);
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(int userId, [FromBody] int prodID, int qty)
        {
            var user = await cartService.AddToCart(userId, prodID, qty);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        [HttpDelete("DeleteCartItems")]
        public async Task<IActionResult> Delete(int userId, [FromBody] int prodID)
        {
            try
            {
                var itm = await cartService.RemoveFromCart(userId,prodID);
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

        [HttpDelete("Checkout")]
        public async Task<IActionResult> Checkout(int userId)
        {
            try
            {
                var itm = await cartService.DoCheckout(userId);
                if (!itm)
                {
                    return BadRequest();
                }
                return Ok("Item Moved to Order List");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
