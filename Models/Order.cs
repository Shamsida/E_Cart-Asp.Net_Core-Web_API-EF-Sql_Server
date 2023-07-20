using System.ComponentModel.DataAnnotations;

namespace E_cart.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public decimal Total { get; set; }

        public List<OrderDetail> OrderDetail { get; set; }
    }
}
