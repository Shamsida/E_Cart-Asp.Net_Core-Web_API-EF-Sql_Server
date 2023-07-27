using AutoMapper;
using E_cart.Data;
using E_cart.DTO;
using E_cart.Models;
using E_cart.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_cart.Repository
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        //private string secretkey;

        public UserService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
           // this.secretkey = configuration.GetValue<string>("Jwt:Key");
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> Get()
        {
            try
            {
                var items = await _dataContext.Users
                            .Include(u => u.Carts)
                            .ThenInclude(c => c.CartDetails)
                            .ToListAsync();

                var usrDtos = items.Select(u => new UserDTO
                {
                    UserId = u.Id,
                    Username = u.Username,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    Email = u.Email,
                    Number = u.Number,
                    Carts = u.Carts.Select(c => new CartDTO
                    {
                        CartId = c.Id,
                        CartDetails = c.CartDetails?.Select(cd => new CartDetailDTO
                        {
                            CartDetailId = cd.Id,
                            CartId = cd.CartId,
                            ProductId = cd.ProductId,
                            Quantity = cd.Quantity,
                            UnitPrice = cd.UnitPrice,
                            Total = cd.Total
                        }).ToList() ?? new List<CartDetailDTO>()
                    }).ToList()
                }).ToList();

                //var usrDto = _mapper.Map<List<UserDTO>>(items);
                return usrDtos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UserDTO> GetById(int id)
        {
            try
            {
                var items = await _dataContext.Users
                            .Include(u => u.Carts)
                            .ThenInclude(c => c.CartDetails)
                            .FirstOrDefaultAsync(u => u.Id == id);

                if (items == null)
                {
                    throw new Exception("Invalid entry");
                }

                var usrDtos = new UserDTO
                {
                    UserId = items.Id,
                    Username = items.Username,
                    Firstname = items.Firstname,
                    Lastname = items.Lastname,
                    Email = items.Email,
                    Number = items.Number,
                    Carts = items.Carts.Select(c => new CartDTO
                    {
                        CartId = c.Id,
                        CartDetails = c.CartDetails?.Select(cd => new CartDetailDTO
                        {
                            CartDetailId = cd.Id,
                            CartId = cd.CartId,
                            ProductId = cd.ProductId,
                            Quantity = cd.Quantity,
                            UnitPrice = cd.UnitPrice,
                            Total = cd.Total
                        }).ToList() ?? new List<CartDetailDTO>()
                    }).ToList()
                };

                //var usrDto = _mapper.Map<List<UserDTO>>(items);
                return usrDtos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<LoginResDTO> Login(LoginReqDTO loginReq)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResDTO> AdminLogin(LoginReqDTO loginReq)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> SignUP(User employee)
        {
            throw new NotImplementedException();
        }
    }
}
