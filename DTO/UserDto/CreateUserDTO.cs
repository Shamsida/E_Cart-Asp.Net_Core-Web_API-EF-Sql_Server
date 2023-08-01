using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_cart.DTO.UserDto
{
    public class CreateUserDTO
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
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
        public string Imageurl { get; set; } = null!;

        [Required]
        public decimal Number { get; set; } = 0;
    }
}
