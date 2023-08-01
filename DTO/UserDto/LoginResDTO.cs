using E_cart.Models;

namespace E_cart.DTO.UserDto
{
    public class LoginResDTO
    {
        public User user { get; set; }
        public string Token { get; set; }
    }
}
