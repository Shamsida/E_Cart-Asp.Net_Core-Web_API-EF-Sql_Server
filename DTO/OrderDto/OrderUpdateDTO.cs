using System.ComponentModel.DataAnnotations;

namespace E_cart.DTO.OrderDto
{
    public class OrderUpdateDTO
    {
        public string PickupAddress { get; set; }
        public double PickupPhoneNumber { get; set; }
        public string PickupEmail { get; set; }
        public string? Status { get; set; }
    }
}
