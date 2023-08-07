using E_cart.DTO.ProductDto;
using E_cart.DTO.UserDto;
using E_cart.Models;

namespace E_cart.DTO.CartDto
{
    public class CartDataDTO
    {
        public int CartId { get; set; }

        public decimal TotalPrice { get; set; }
        public UserDataDTO User { get; set; }
        public List<CartDetailDTO> CartDetails { get; set; }
    }
}
