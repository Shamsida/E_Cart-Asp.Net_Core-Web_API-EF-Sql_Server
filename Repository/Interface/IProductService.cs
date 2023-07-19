using E_cart.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_cart.Repository.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> Get();
        Task<Product> Get(int id);
        Task<Product> Post(Product item);
        Task<Product> Put(int Id, Product item);
        Task<bool> Delete(int Id);
    }
}
