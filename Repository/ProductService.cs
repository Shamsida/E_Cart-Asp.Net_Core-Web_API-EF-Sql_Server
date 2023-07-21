using AutoMapper;
using E_cart.Data;
using E_cart.Models;
using E_cart.Models.DTO;
using E_cart.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_cart.Repository
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper mapper;
        public ProductService(DataContext dataContext,IMapper mapper)
        {
            _dataContext = dataContext;
            mapper = mapper;
            
        }


        public async Task<IEnumerable<Product>> Get()
        {
            try
            {
                var items = await _dataContext.Products.Include(e=>e.Category).ToListAsync();
                return items;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Product> GetById(int id)
        {
            try
            {
                var items = await _dataContext.Products.Include(e => e.Category).Where(e => e.Id == id).FirstOrDefaultAsync();
                if (items == null)
                {
                    throw new Exception("Invalid entry");
                }
                return items;
            }
            catch (Exception ex)
            {
                throw;
                return null;
            }
        }

        public async Task<Product> Post(CreateProductDTO item)
        {
            try
            {
                if (item == null)
                {
                    throw new Exception("Invalid entry");
                }
                var category = await _dataContext.Categories.FindAsync(item.CategoryId);
                if (category == null)
                {
                    throw new Exception("Invalid category ID.");
                }

                Product pro = new Product()
                {
                    CategoryId = item.CategoryId,
                    Title = item.Title,
                    Description = item.Description,
                    Image = item.Image,
                    Price = item.Price,
                    //Category =await _dataContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == item.CategoryId)
                    Category = category,
                };
                //var product = mapper.Map<Product>(item);
                _dataContext.Products.Add(pro);
                await _dataContext.SaveChangesAsync();
                return pro;
            }
            catch (Exception ex)
            {
                throw;
                return null;
            }
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
