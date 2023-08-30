using E_cart.Models;
using System.ComponentModel.DataAnnotations;

namespace E_cart.DTO.OrderDto
{
    public class OrderCreateDTO
    {
        public string? PickupAddress { get; set; }
        public double? PickupPhoneNumber { get; set; }
        public string? PickupEmail { get; set; }

        public decimal TotalPrice { get; set; }
        public int UserId { get; set; }
        public string? StripePaymentIntentId { get; set; }
        public string? Status { get; set; }
        public int TotalItems { get; set; }
        public string? PaymentStatus { get; set; }
        //public List<OrderDetailCreateDTO> OrderDetails { get; set; }
    }
}
