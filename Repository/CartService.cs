using AutoMapper;
using E_cart.Data;
using E_cart.DTO.CartDto;
using E_cart.DTO.ProductDto;
using E_cart.DTO.UserDto;
using E_cart.Models;
using E_cart.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_cart.Repository
{
    public class CartService : ICartService
    {
        private readonly DataContext _dataContext;

        public CartService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<CartDataDTO>> GetCart(int userId)
        {
            try
            {
                var user = await _dataContext.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                var cart = await _dataContext.Carts
                            .Where(c => c.User.Id == userId)
                            .Include(c => c.CartDetails)
                            .ThenInclude(c => c.Product)
                            .ToListAsync();

                if (cart == null || !cart.Any())
                {
                    throw new Exception("Cart not found.");
                }

                var cartDataDTOList = cart.Select(c => new CartDataDTO
                {
                    CartId = c.Id,
                    TotalPrice = c.TotalPrice,
                    //User = new UserDataDTO
                    //{
                    //    UserId = c.User.Id,
                    //    Username = c.User.Username,
                    //    Firstname = c.User.Firstname,
                    //    Lastname = c.User.Lastname,
                    //    Email = c.User.Email,
                    //    Role = c.User.Role,
                    //    Number = c.User.Number
                    //},
                    CartDetails = c.CartDetails.Select(cd => new CartDetailDTO
                    {
                        CartDetailId = cd.Id,
                        CartId = cd.CartId,
                        ProductId = cd.ProductId,
                        Quantity = cd.Quantity,
                        UnitPrice = cd.UnitPrice,
                        Total = cd.Total,
                        Product = new ProductDTO 
                        {
                            Id = cd.Product.Id,
                            CategoryId = cd.Product.CategoryId,
                            Title = cd.Product.Title,
                            Description = cd.Product.Description,
                            Price = cd.Product.Price,
                            Image = cd.Product.Image
                        }
                    }).ToList()
                });

                return cartDataDTOList;
            }
            catch (Exception ex)
            {
                throw;
                return null;
            }
        }

        public async Task<Cart> AddToCart(int userId, [FromBody] AddtoCartDTO itm)
        {
            try
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                var cart = await _dataContext.Carts
                            .Include(c => c.CartDetails)
                            .FirstOrDefaultAsync(c => c.User.Id == userId);

                if (cart == null)
                {
                    cart = new Cart { User = user };
                    _dataContext.Carts.Add(cart);
                    await _dataContext.SaveChangesAsync();
                }


                var cartItem = await _dataContext.CartDetails
                            .FirstOrDefaultAsync(a => a.CartId == cart.Id && a.ProductId == itm.ProdID);

                var product = await _dataContext.Products.FirstOrDefaultAsync(a => a.Id == itm.ProdID);

                if (product == null)
                {
                    throw new Exception("Product not found.");
                }

                if (cartItem == null)
                {
                   
                    var cartDetailEntity = new CartDetail
                    {
                        CartId = cart.Id,
                        ProductId = itm.ProdID,
                        Quantity = itm.Qty,
                        UnitPrice = product.Price,
                        Total = itm.Qty * product.Price
                    };
                    if (cart.CartDetails == null)
                    {
                        cart.CartDetails = new List<CartDetail>();
                    }

                    cart.CartDetails.Add(cartDetailEntity);
                    await _dataContext.SaveChangesAsync();
                }
                else
                {
                    cartItem.Quantity += itm.Qty;
                    cartItem.Total += itm.Qty * product.Price;
                    await _dataContext.SaveChangesAsync();
                }

                cart.TotalPrice = cart.CartDetails.Sum(cd => cd.Total);
                _dataContext.SaveChanges();

                return cart;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> RemoveFromCart(int userId, int prodID)
        {
            try
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                var cart = await _dataContext.Carts
                            .Include(c => c.CartDetails)
                            .FirstOrDefaultAsync(c => c.User.Id == userId);

                if (cart == null)
                {
                    throw new Exception("Invalid Cart.");
                }
                var cartDetail = await _dataContext.CartDetails
                                 .FirstOrDefaultAsync(cd => cd.CartId == cart.Id && cd.ProductId == prodID);

                var product = await _dataContext.Products.FirstOrDefaultAsync(a => a.Id == prodID);

                if (cartDetail == null)
                {
                    throw new Exception("Item Not Found.");
                } 
                else if (cartDetail.Quantity == 1)
                {
                    _dataContext.CartDetails.Remove(cartDetail);
                    await _dataContext.SaveChangesAsync();
                }
                else
                {
                    cartDetail.Quantity = cartDetail.Quantity - 1;
                    cartDetail.Total = cartDetail.Total - product.Price;
                    _dataContext.SaveChanges();
                }

                cart.TotalPrice = cart.CartDetails.Sum(cd => cd.Total);
                await _dataContext.SaveChangesAsync();

                if (cart.CartDetails.Count == 0)
                {
                    _dataContext.Carts.Remove(cart);
                    await _dataContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }
        }

        public async Task<bool> DoCheckout(int userId)
        {
            try
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    throw new Exception("User not found.");
                }
                var cart = await _dataContext.Carts
                            .Include(c => c.CartDetails)
                            .FirstOrDefaultAsync(c => c.User.Id == userId);
                if (cart == null)
                {
                    throw new Exception("Invalid Cart.");
                }
                var cartDetail = await _dataContext.CartDetails
                                 .Where(cd => cd.CartId == cart.Id).ToListAsync();
                if (cartDetail.Count == 0)
                {
                    throw new Exception("Cart is empty");
                }

                var existingOrder = await _dataContext.Orders
                            .Include(o => o.OrderDetail)
                            .FirstOrDefaultAsync(o => o.UserId == userId);


                if (existingOrder == null)
                {
                    var order = new Order
                    {
                        UserId = userId,
                        CreateDate = DateTime.UtcNow,
                        TotalPrice = cart.TotalPrice,
                        OrderDetail = new List<OrderDetail>()
                    };
                    foreach (var cartItem in cartDetail)
                    {
                        var orderDetail = new OrderDetail
                        {
                            ProductId = cartItem.ProductId,
                            Quantity = cartItem.Quantity,
                            UnitPrice = cartItem.UnitPrice,
                            Total = cartItem.Quantity * cartItem.UnitPrice
                        };

                        order.OrderDetail.Add(orderDetail);
                        await _dataContext.SaveChangesAsync();
                    }
                    _dataContext.Orders.Add(order);
                    await _dataContext.SaveChangesAsync();
                }
                else
                {
                    foreach (var cartItem in cartDetail)
                    {
                        var existingOrderDetail = existingOrder.OrderDetail
                                                 .FirstOrDefault(od => od.ProductId == cartItem.ProductId);

                        if (existingOrderDetail == null)
                        {
                            var orderDetail = new OrderDetail
                            {
                                ProductId = cartItem.ProductId,
                                Quantity = cartItem.Quantity,
                                UnitPrice = cartItem.UnitPrice,
                                Total = cartItem.Quantity * cartItem.UnitPrice
                            };

                            existingOrder.OrderDetail.Add(orderDetail);
                            await _dataContext.SaveChangesAsync();
                        }
                        else
                        {
                            existingOrderDetail.Quantity += cartItem.Quantity;
                            existingOrderDetail.Total += cartItem.Quantity * cartItem.UnitPrice;
                            await _dataContext.SaveChangesAsync();
                        }
                    }
                }

                _dataContext.CartDetails.RemoveRange(cartDetail);
                await _dataContext.SaveChangesAsync();

                if (cart.CartDetails.Count == 0)
                {
                    _dataContext.Carts.Remove(cart);
                    await _dataContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }

        }

        public async Task<bool> IncreaseQuantity(int userId, int ProdID)
        {
            try
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                var cart = await _dataContext.Carts
                            .Include(c => c.CartDetails)
                            .FirstOrDefaultAsync(c => c.User.Id == userId);

                if (cart == null)
                {
                    throw new Exception("Cart not found.");
                }

                var cartItem = cart.CartDetails.FirstOrDefault(a => a.ProductId == ProdID);

                if (cartItem == null)
                {
                    throw new Exception("Product not found in cart.");
                }

                cartItem.Quantity++;
                cartItem.Total = cartItem.Quantity * cartItem.UnitPrice;

                cartItem.Cart.TotalPrice = cartItem.Cart.CartDetails.Sum(cd => cd.Total);

                await _dataContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }
        }

        public async Task<bool> DecreaseQuantity(int userId, int ProdID)
        {
            try
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                var cart = await _dataContext.Carts
                            .Include(c => c.CartDetails)
                            .FirstOrDefaultAsync(c => c.User.Id == userId);

                if (cart == null)
                {
                    throw new Exception("Cart not found.");
                }

                var cartItem = cart.CartDetails.FirstOrDefault(a => a.ProductId == ProdID);

                if (cartItem == null)
                {
                    throw new Exception("Product not found in cart.");
                }

                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    cartItem.Total -= cartItem.UnitPrice;
                    await _dataContext.SaveChangesAsync();

                    cart.TotalPrice -= cartItem.UnitPrice;
                    await _dataContext.SaveChangesAsync();
                }
                else
                {
                    cart.CartDetails.Remove(cartItem);
                    _dataContext.CartDetails.Remove(cartItem);
                    cart.TotalPrice = cart.CartDetails.Sum(cd => cd.Total);
                    await _dataContext.SaveChangesAsync();
                }

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
