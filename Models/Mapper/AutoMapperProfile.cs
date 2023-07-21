using AutoMapper;
using E_cart.Models.DTO;
using System.Runtime.CompilerServices;

namespace E_cart.Models.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() { 
            CreateMap<Product,CreateProductDTO>().ReverseMap();
        }
    }
}
