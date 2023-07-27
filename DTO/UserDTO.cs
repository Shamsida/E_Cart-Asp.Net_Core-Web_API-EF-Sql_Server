using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_cart.Models;

namespace E_cart.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? Username { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public string? Email { get; set; }

        public decimal Number { get; set; } = 0;

        public List<CartDTO> Carts { get; set; }
    }
}
