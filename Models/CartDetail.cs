using System.ComponentModel.DataAnnotations;

namespace E_cart.Models
{
    public class CartDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CartId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
