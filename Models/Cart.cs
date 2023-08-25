using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_cart.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [NotMapped]
        public string? StripePaymentIntentId { get; set; }

        public decimal TotalPrice { get; set; }
        public int TotalItems { get; set; }

        [NotMapped]
        public string ClientSecret { get; set; }

        [Required]
        public User User { get; set; }

        public ICollection<CartDetail>? CartDetails { get; set; } = new List<CartDetail>();
    }
}
