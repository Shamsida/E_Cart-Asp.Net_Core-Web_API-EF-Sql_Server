using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_cart.DTO.UserDto
{
    public class CreateUserDTO
    {
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

        public IFormFile? Imageurl { get; set; }

        [Required]
        public double Number { get; set; } = 0;
    }
}
