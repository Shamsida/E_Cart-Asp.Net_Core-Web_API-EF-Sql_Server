using E_cart.DTO;
using E_cart.Models;

namespace E_cart.Repository.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> Get();
        Task<UserDTO> GetById(int id);
        Task<UserDTO> SignUP(User employee);
        Task<LoginResDTO> Login(LoginReqDTO loginReq);
        Task<LoginResDTO> AdminLogin(LoginReqDTO loginReq);
    }
}
