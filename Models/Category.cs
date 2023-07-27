using System.ComponentModel.DataAnnotations;

namespace E_cart.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        //public List<Product> Products { get; set; }
    }
}
