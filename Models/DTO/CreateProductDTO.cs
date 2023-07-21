using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_cart.Models.DTO
{
    public class CreateProductDTO
    {
        public int CategoryId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
    }
}
