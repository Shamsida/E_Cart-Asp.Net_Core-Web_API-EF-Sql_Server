﻿namespace E_cart.DTO
{
    public class CartDTO
    {
        public int CartId { get; set; }
        public List<CartDetailDTO> CartDetails { get; set; }
    }
}
