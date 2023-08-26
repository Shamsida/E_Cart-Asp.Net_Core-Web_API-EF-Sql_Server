using E_cart.DTO.CartDto;
using E_cart.DTO.OrderDto;
using E_cart.Repository;
using E_cart.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet("GetOrderById")]
        public async Task<IActionResult> UserOrders(int userId)
        {
            var itm = await orderService.UserOrders(userId);
            if (itm == null)
            {
                return NotFound();
            }
            return Ok(itm);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            var itm = await orderService.GetOrders();
            if (itm == null)
            {
                return NotFound();
            }
            return Ok(itm);
        }

        [HttpPost("AddToOrder")]
        public async Task<IActionResult> AddToOrder(int userId, [FromBody] OrderCreateDTO itm)
        {
            var user = await orderService.AddToOrder(userId, itm);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
    }
}
