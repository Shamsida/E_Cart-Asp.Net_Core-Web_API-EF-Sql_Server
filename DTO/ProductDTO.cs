using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_cart.Models;

namespace E_cart.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public  Category Category { get; set; }
    }
}
