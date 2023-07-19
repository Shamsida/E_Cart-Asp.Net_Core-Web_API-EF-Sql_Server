using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_cart.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Username { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Firstname { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Imageurl { get; set; }
        public decimal Number { get; set; }
    }
}
