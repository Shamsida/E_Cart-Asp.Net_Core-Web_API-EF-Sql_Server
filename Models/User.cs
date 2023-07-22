using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_cart.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string? Firstname { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string? Lastname { get; set; }

        [Required]
        public string? Email { get; set; }
        public string? Role { get; set; }

        [Required]
        public string? Password { get; set; }
        public string? Imageurl { get; set; }

        [Required]
        public decimal Number { get; set; } =0;

        public virtual List<Cart> Carts { get; set; }
        public virtual List<Order> Orders { get; set; } 
        public virtual List<WishList> WishList { get; set; }
    }
}
