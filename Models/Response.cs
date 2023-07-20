namespace E_cart.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string? StatusMessage { get; set; }
        public List<User> listUser { get; set; }
        public User user { get; set; }
        public List<Product> listproduct { get; set; }
        public Product product { get; set; }
    }
}
