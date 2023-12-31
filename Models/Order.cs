﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_cart.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PickupAddress { get; set; }
        public double PickupPhoneNumber { get; set; }
        public string PickupEmail { get; set; }

        public string? StripePaymentIntentId { get; set; }

        public decimal TotalPrice { get; set; }

        [Required]
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public string? Status { get; set; }
        public int TotalItems { get; set; }
        public string? PaymentStatus { get; set; }

        public List<OrderDetail> OrderDetail { get; set; }
    }
}
