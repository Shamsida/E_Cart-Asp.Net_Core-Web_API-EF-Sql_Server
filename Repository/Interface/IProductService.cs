﻿using E_cart.Models;
using E_cart.DTO.ProductDto;
using Microsoft.AspNetCore.Mvc;

namespace E_cart.Repository.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> Get();
        Task<Product> GetById(int id);
        Task<IEnumerable<ProductDTO>> GetProductsByCategory(string categoryName);
        Task<Product> Post([FromForm] CreateProductDTO item);
        Task<Product> Put(int Id, UpdateProductDTO item);
        Task<bool> Delete(int Id);

        Task <bool> DeleteImage(string imageFileName);
    }
}
