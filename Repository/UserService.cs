using AutoMapper;
using E_cart.Data;
using E_cart.DTO.UserDto;
using E_cart.Models;
using E_cart.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_cart.Repository
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment environment;
        private readonly SaveImage saveImage;
        private readonly string secretkey;

        public UserService(DataContext dataContext, IConfiguration configuration, IMapper mapper, IWebHostEnvironment env, SaveImage saveImage)
        {
            _dataContext = dataContext;
            this.secretkey = configuration.GetValue<string>("Jwt:Key");
            _mapper = mapper;
            this.environment = env;
            this.saveImage = saveImage;
        }

        public async Task<IEnumerable<UserDTO>> Get()
        {
            try
            {
                var items = await _dataContext.Users
                            .Include(u => u.Carts)
                            .ThenInclude(c => c.CartDetails)
                            .Where(x => x.Role == "user")
                            .ToListAsync();

                var usrDtos = items.Select(u => new UserDTO
                {
                    UserId = u.Id,
                    Username = u.Username,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    Email = u.Email,
                    Role = u.Role,
                    Imageurl = u.Imageurl,
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
                            .Where(x => x.Role == "user")
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
                    Role = items.Role,
                    Imageurl = items.Imageurl,
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

        public async Task<UserDTO> GetByUsername(string username)
        {
            try
            {
                var items = await _dataContext.Users
                            .Include(u => u.Carts)
                            .ThenInclude(c => c.CartDetails)
                            .Where(x => x.Role == "user")
                            .FirstOrDefaultAsync(u => u.Username == username);

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
                    Role = items.Role,
                    Imageurl = items.Imageurl,
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

        public async Task<LoginResDTO> Login(LoginReqDTO loginReq)
        {
            try
            {
                if (loginReq == null)
                {
                    throw new Exception("Invalid Enrty");
                };
                var usr = await _dataContext.Users
                          .SingleOrDefaultAsync(e => e.Username.ToLower() == loginReq.Username.ToLower() && e.Role == "user");

                //bool isPasswordValid = BCrypt.Net.BCrypt.Verify(usr.Password, loginReq.Password);

                if (usr == null || !BCrypt.Net.BCrypt.Verify(loginReq.Password, usr.Password))
                {
                    throw new Exception("Invalid user name or password");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = Encoding.ASCII.GetBytes(secretkey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, usr.Username),
                    new Claim(ClaimTypes.Role,usr.Role)
                    }),

                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new(new SymmetricSecurityKey(token), SecurityAlgorithms.HmacSha256)
                };
                var jwttoken = tokenHandler.CreateToken(tokenDescriptor);
                LoginResDTO loginResDTO = new LoginResDTO()
                {
                    user = usr,
                    Token = tokenHandler.WriteToken(jwttoken)

                };
                return loginResDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<LoginResDTO> AdminLogin(LoginReqDTO loginReq)
        {
            try
            {
                if (loginReq == null)
                {
                    throw new Exception("Invalid Enrty");
                };
                var admn = await _dataContext.Users
                          .SingleOrDefaultAsync(e => e.Email.ToLower() == loginReq.Username.ToLower() && e.Password == loginReq.Password && e.Role == "admin");

                if (admn == null)
                {
                    throw new Exception("Invalid user name or password");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = Encoding.ASCII.GetBytes(secretkey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, admn.Username),
                    new Claim(ClaimTypes.Role,admn.Role)
                    }),

                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new(new SymmetricSecurityKey(token), SecurityAlgorithms.HmacSha256)
                };
                var jwttoken = tokenHandler.CreateToken(tokenDescriptor);
                LoginResDTO loginResDTO = new LoginResDTO()
                {
                    user = admn,
                    Token = tokenHandler.WriteToken(jwttoken)

                };
                return loginResDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UserDataDTO> SignUP(CreateUserDTO usr)
        {
            try
            {
                if (usr == null)
                {
                    throw new Exception("Invalid entry");
                }
               // string uniqueFileName = Guid.NewGuid().ToString() + "_" + usr.Imageurl.FileName;

                // Combine the path to the "images" folder with the unique filename
               // string imagePath = Path.Combine(environment.WebRootPath, "images", uniqueFileName);

                // Save the image to the "images" folder
               // using (var fileStream = new FileStream(imagePath, FileMode.Create))
               // {
                //    usr.Imageurl.CopyTo(fileStream);
                //}
                var user = new User
                {
                    Username = usr.Username,
                    Firstname = usr.Firstname,
                    Lastname = usr.Lastname,
                    Email = usr.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(usr.Password, 10),
                    Imageurl = usr.Imageurl,
                    //Imageurl = await saveImage.SaveImages(usr.Imageurl, "image"),
                    //Imageurl = "/images/" + uniqueFileName,
                    Number = usr.Number,
                    Role = "user"
                };

                _dataContext.Users.Add(user);
                await _dataContext.SaveChangesAsync();
                var usrDto = _mapper.Map<UserDataDTO>(user);
                return usrDto;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
