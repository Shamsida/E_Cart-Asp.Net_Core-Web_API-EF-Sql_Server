using E_cart.DTO.UserDto;
using E_cart.Models;

namespace E_cart.Repository.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> Get();
        Task<UserDTO> GetById(int id);
        Task<UserDataDTO> SignUP(CreateUserDTO usr);
        Task<LoginResDTO> Login(LoginReqDTO loginReq);
        Task<LoginResDTO> AdminLogin(LoginReqDTO loginReq);
    }
}
