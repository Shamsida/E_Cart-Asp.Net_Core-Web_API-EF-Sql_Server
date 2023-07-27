using E_cart.Models;
using E_cart.DTO;
using Microsoft.AspNetCore.Mvc;

namespace E_cart.Repository.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> Get();
        Task<Product> GetById(int id);
        Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryId);
        Task <Product> Post(CreateProductDTO item);
        Task<Product> Put(int Id, UpdateProductDTO item);
        Task<bool> Delete(int Id);
    }
}
