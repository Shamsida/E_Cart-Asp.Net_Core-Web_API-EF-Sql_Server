using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_cart.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        [Required]
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public List<OrderDetail> OrderDetail { get; set; }
    }
}
