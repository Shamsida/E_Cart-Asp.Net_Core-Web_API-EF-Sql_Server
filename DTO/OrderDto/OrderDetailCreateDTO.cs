using System.ComponentModel.DataAnnotations;

namespace E_cart.DTO.OrderDto
{
    public class OrderDetailCreateDTO
    {

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}
