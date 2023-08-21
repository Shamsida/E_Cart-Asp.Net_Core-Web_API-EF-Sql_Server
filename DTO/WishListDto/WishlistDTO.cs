using E_cart.DTO.ProductDto;
using System.ComponentModel.DataAnnotations;

namespace E_cart.DTO.WishListDto
{
    public class WishlistDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ProductDTO Product { get; set; }
    }
}
