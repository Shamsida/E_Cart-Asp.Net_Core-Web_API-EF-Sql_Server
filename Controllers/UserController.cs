using E_cart.DTO.UserDto;
using E_cart.Models;
using E_cart.Repository;
using E_cart.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_cart.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("api/user/GetUsers")]
        public async Task<IActionResult> Get()
        {
            var itm = await userService.Get();
            if (itm == null)
            {
                return BadRequest();
            }
            return Ok(itm);
        }

        [HttpGet("api/user/GetUsersById")]
        public async Task<IActionResult> GetById(int id)
        {
            var itm = await userService.GetById(id);
            if (itm == null)
            {
                return NotFound();
            }
            return Ok(itm);
        }

        [HttpGet("api/user/GetUsersByUsername")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var itm = await userService.GetByUsername(username);
            if (itm == null)
            {
                return NotFound();
            }
            return Ok(itm);
        }

        [HttpPost("api/user/Signup")]
        public async Task<IActionResult> SignUp(CreateUserDTO usr)
        {
            var user = await userService.SignUP(usr);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        [HttpPost("api/user/Login")]
        public async Task<ActionResult> Login(LoginReqDTO loginReq)
        {
            var emp = await userService.Login(loginReq);
            if (emp == null)
            {
                return BadRequest("Inavlid Credential");
            }
            return Ok(emp);
        }

        [HttpPost("api/admin/Login")]
        public async Task<ActionResult> AdminLogin(LoginReqDTO loginReq)
        {
            var emp = await userService.AdminLogin(loginReq);
            if (emp == null)
            {
                return BadRequest("Inavlid Credential");
            }
            return Ok(emp);
        }
    }
}
