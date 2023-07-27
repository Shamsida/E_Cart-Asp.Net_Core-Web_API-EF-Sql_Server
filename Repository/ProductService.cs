using AutoMapper;
using E_cart.Data;
using E_cart.Models;
using E_cart.DTO;
using E_cart.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_cart.Repository
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public ProductService(DataContext dataContext,IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ProductDTO>> Get()
        {
            try
            {
                var items = await _dataContext.Products
                           .Include(e=>e.Category)
                           .ToListAsync();

                var prodDto = _mapper.Map<List<ProductDTO>>(items);
                return prodDto;
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
                var items = await _dataContext.Products
                            .Include(e => e.Category)
                            .Where(e => e.Id == id)
                            .FirstOrDefaultAsync();

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

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryId)
        {
            try
            {
                var products = await _dataContext.Products
                               .Include(p => p.Category)
                               .Where(p => p.CategoryId == categoryId)
                               .ToListAsync();

                if (products == null || products.Count == 0)
                {
                    throw new Exception("Invalid entry");
                }
                var prodDto = _mapper.Map<List<ProductDTO>>(products);
                return prodDto;

            }
            catch (Exception ex)
            {
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

        public async Task<Product> Put(int Id, UpdateProductDTO item)
        {
            try
            {
                if (item == null)
                {
                    throw new Exception("Invalid entry");
                }
                var itm = _dataContext.Products.FirstOrDefault(x => x.Id == Id);
                if (itm == null)
                {
                    throw new Exception("Not Found");
                }
                itm.Title = item.Title;
                itm.Price = item.Price;
                itm.CategoryId = item.CategoryId;

                var category = await _dataContext.Categories.FindAsync(item.CategoryId);

                if (category == null)
                {
                    throw new Exception("Invalid category ID.");
                }
                itm.Category = category;
                await _dataContext.SaveChangesAsync();
                return itm;

            }
            catch (Exception ex)
            {
                throw;
                return null;
            }
        }

        public async Task<bool> Delete(int Id)
        {
            try
            {
                var itm = _dataContext.Products.FirstOrDefault(x => x.Id == Id);
                if (itm == null)
                {
                    throw new Exception("Not Found");
                }
                _dataContext.Products.Remove(itm);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }
        }
    }
}
