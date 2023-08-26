using E_cart.DTO.UserDto;
using E_cart.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_cart.Repository.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> Get();
        Task<UserDTO> GetById(int id);
        Task<UserDTO> GetByUsername(string username);
        Task<UserDataDTO> SignUP([FromForm] CreateUserDTO usr);
        Task<LoginResDTO> Login(LoginReqDTO loginReq);
        Task<LoginResDTO> AdminLogin(LoginReqDTO loginReq);
        Task<UserDataDTO> AdminSignUP([FromForm] CreateUserDTO usr);
        Task<bool> Delete(int Id);
    }
}
