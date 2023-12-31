﻿using Azure;
using E_cart.Data;
using E_cart.DTO.OrderDto;
using E_cart.DTO.ProductDto;
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

        public async Task<IEnumerable<Order>> UsersOrder(int Id)
        {
            try
            {
                var order = await _dataContext.Orders.Where(u => u.Id == Id).FirstOrDefaultAsync();

                if (order == null)
                {
                    throw new Exception("Order not found.");
                }

                var orders = await _dataContext.Orders
                            .Include(x => x.OrderDetail)
                            .ThenInclude(x => x.Product)
                            .Where(a => a.Id == Id)
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
                            OrderId = order.Id,
                            ProductId = cartItem.ProductId,
                            Quantity = cartItem.Quantity,
                            UnitPrice = cartItem.UnitPrice,
                            Total = cartItem.Quantity * cartItem.UnitPrice
                        };
                        _dataContext.OrderDetails.Add(orderDetail);
                    }
                    _dataContext.SaveChanges();
                    _dataContext.CartDetails.RemoveRange(cartDetail);
                    await _dataContext.SaveChangesAsync();

                    if (cart.CartDetails.Count == 0)
                    {
                        _dataContext.Carts.Remove(cart);
                        await _dataContext.SaveChangesAsync();
                    }
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
        public async Task<Order> Put(int Id, OrderUpdateDTO item)
        {
            try
            {
                if (item == null)
                {
                    throw new Exception("Invalid entry");
                }
                var itm = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == Id);
                if (itm == null)
                {
                    throw new Exception("Order Not Found");
                }

                itm.CreateDate = DateTime.Now;
                itm.Status = item.Status;

                await _dataContext.SaveChangesAsync();
                return itm;

            }
            catch (Exception ex)
            {
                throw;
                return null;
            }
        }

        public async Task<bool> RemoveFromOrder(int userId, int orderID)
        {
            try
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                var order = await _dataContext.Orders
                            .Where(c=> c.Id == orderID)
                            .FirstOrDefaultAsync(c => c.UserId == userId);

                if (order == null)
                {
                    throw new Exception("Invalid Order.");
                }

                _dataContext.Orders.Remove(order);
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
