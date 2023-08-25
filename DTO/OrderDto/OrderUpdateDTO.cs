using System.ComponentModel.DataAnnotations;

namespace E_cart.DTO.OrderDto
{
    public class OrderUpdateDTO
    {
        public int Id { get; set; }
        public string PickupAddress { get; set; }
        public double PickupPhoneNumber { get; set; }
        public string PickupEmail { get; set; }

        public string? StripePaymentIntentId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public string? PaymentStatus { get; set; }
        public string? Status { get; set; }
    }
}
