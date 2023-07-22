using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_cart.Models
{
    public class WishList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
