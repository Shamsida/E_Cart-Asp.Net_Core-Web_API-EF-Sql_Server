using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_cart.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; } 
        public string? Image { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public virtual List<WishList> WishList { get; set; } = new List<WishList>();

    }
}
