using E_cart.Repository;
using E_cart.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> Get()
        {
            var itm = await userService.Get();
            if (itm == null)
            {
                return BadRequest();
            }
            return Ok(itm);
        }

        [HttpGet("GetUsersById")]
        public async Task<IActionResult> GetById(int id)
        {
            var itm = await userService.GetById(id);
            if (itm == null)
            {
                return NotFound();
            }
            return Ok(itm);
        }
    }
}
