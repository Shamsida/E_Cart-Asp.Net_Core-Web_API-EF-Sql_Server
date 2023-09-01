using E_cart.DTO.CartDto;
using E_cart.DTO.OrderDto;
using E_cart.DTO.ProductDto;
using E_cart.Repository;
using E_cart.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

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

        [HttpGet("GetOrderByUserId")]
        public async Task<IActionResult> UserOrders(int userId)
        {
            var itm = await orderService.UserOrders(userId);
            if (itm == null)
            {
                return NotFound();
            }
            return Ok(itm);
        }

        [HttpGet("GetOrderById")]
        public async Task<IActionResult> UsersOrder(int Id)
        {
            var itm = await orderService.UsersOrder(Id);
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

        [HttpPut("PutItems")]
        public async Task<IActionResult> Put(int Id, OrderUpdateDTO item)
        {
            var itm = await orderService.Put(Id, item);
            if (itm == null)
            {
                return BadRequest("Inavlid Credential");
            }
            return Ok(itm);
        }

        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> RemoveFromOrder(int userId, int orderID)
        {
            try
            {
                var itm = await orderService.RemoveFromOrder(userId, orderID);
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
