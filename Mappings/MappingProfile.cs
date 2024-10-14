using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using furniro_server_hari.DTO;
using furniro_server_hari.DTO.AuthDTOs;
using furniro_server_hari.DTO.ProductDTOs;
using furniro_server_hari.Models;

namespace furniro_server_hari.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductDto, Product>().ReverseMap();
            CreateMap<UpdateProductDto, Product>().ReverseMap(); ;
            CreateMap<ProductDto, Product>().ReverseMap(); ;
            // CreateMap<Product, ProductDto>();

            CreateMap<RegisterDTO, User>().ReverseMap(); ;
            CreateMap<LoginDTO, User>().ReverseMap(); ;
            CreateMap<UserDTO, User>().ReverseMap(); ;
            // CreateMap<User, UserDTO>();
        }
    }
}