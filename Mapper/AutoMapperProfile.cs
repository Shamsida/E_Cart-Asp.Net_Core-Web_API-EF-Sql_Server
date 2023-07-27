using AutoMapper;
using E_cart.DTO;
using E_cart.Models;
using System.Runtime.CompilerServices;

namespace E_cart.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() { 
            CreateMap<Product,CreateProductDTO>().ReverseMap();
            CreateMap<Product,UpdateProductDTO>().ReverseMap();
            CreateMap<Product,ProductDTO>().ReverseMap();
            CreateMap<User,UserDTO>().ReverseMap();

        }
    }
}
