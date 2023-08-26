using Azure;
using E_cart.Data;
using E_cart.DTO.OrderDto;
using E_cart.Models;
using E_cart.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace E_cart.Repository
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;

        public OrderService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<Order>> UserOrders(int userId)
        {
            try
            {
                var user = await _dataContext.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                var orders = await _dataContext.Orders
                            .Include(x => x.OrderDetail)
                            .ThenInclude(x => x.Product)
                            .Where(a => a.UserId == userId)
                            .ToListAsync();
                return orders;
            }
            catch (Exception ex)
            {
                throw;
                return null;
            }
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            try
            {
                var orders = await _dataContext.Orders
                            .Include(x => x.OrderDetail)
                            .ThenInclude(x => x.Product)
                            .ToListAsync();

                if (orders.Count == 0)
                {
                    throw new Exception("No data found");
                }
                return orders;
            }
            catch (Exception ex)
            {
                throw;
                return null;
            }
        }

        public async Task<Order> AddToOrder(int userId, [FromBody] OrderCreateDTO itm)
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
                    throw new Exception("CartItem is empty");
                }

                Order order = new()
                {
                    UserId = userId,
                    PickupEmail = itm.PickupEmail,
                    PickupAddress = itm.PickupAddress,
                    PickupPhoneNumber = (double)itm.PickupPhoneNumber,
                    TotalPrice = itm.TotalPrice,
                    CreateDate = DateTime.Now,
                    StripePaymentIntentId = itm.StripePaymentIntentId,
                    TotalItems = itm.TotalItems,
                    Status = String.IsNullOrEmpty(itm.Status) ? "pending" : itm.Status,
                    PaymentStatus = itm.PaymentStatus,
                    OrderDetail = new List<OrderDetail>()
                };
                if (itm.TotalItems > 0)
                {
                    _dataContext.Orders.Add(order);
                    _dataContext.SaveChanges();
                    foreach (var cartItem in cartDetail)
                    {
                        var orderDetail = new OrderDetail
                        {
                            ProductId = cartItem.ProductId,
                            Quantity = cartItem.Quantity,
                            UnitPrice = cartItem.UnitPrice,
                            Total = cartItem.Quantity * cartItem.UnitPrice
                        };
                        _dataContext.OrderDetails.Add(orderDetail);
                    }
                    _dataContext.SaveChanges();
                    //_response.Result = order;
                    //order.OrderDetails = null;
                    //_response.StatusCode = HttpStatusCode.Created;
                }
                return order;
            }

            catch (Exception ex)
            {
                throw;
                return null;
            }
        }
    }
}
