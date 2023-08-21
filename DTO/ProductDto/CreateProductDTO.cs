using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_cart.DTO.ProductDto
{
    public class CreateProductDTO
    {
        public string? CategoryName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }
    }
}
