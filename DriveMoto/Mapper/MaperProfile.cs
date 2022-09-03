using DriveMoto.Models;
using System.Security.Claims;
using AutoMapper;

namespace DriveMoto.Mapper
{
    public class MaperProfile : Profile
    {
        public MaperProfile()
        {
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            //CreateMap<Client, ClientDTO>().
            //    ForMember(do)
        }
    }
}
