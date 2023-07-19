using E_cart.Data;
using E_cart.Models;
using E_cart.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_cart.Repository
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<IEnumerable<Product>> Get()
        {
            try
            {
                var items = await _dataContext.Products.ToListAsync();
                return items;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<Product> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Post(Product item)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Put(int Id, Product item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
