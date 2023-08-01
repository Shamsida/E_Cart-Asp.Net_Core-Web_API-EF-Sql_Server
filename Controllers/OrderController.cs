﻿using E_cart.Repository.Interface;
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
    }
}
