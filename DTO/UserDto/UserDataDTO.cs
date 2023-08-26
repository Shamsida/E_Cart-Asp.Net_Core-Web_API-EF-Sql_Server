namespace E_cart.DTO.UserDto
{
    public class UserDataDTO
    {
        public int UserId { get; set; }
        public string? Username { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public string? Email { get; set; }

        public string? Role { get; set; }

        public double Number { get; set; } = 0;
    }
}
