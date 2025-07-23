using AutoMapper;
using TShop.Application.DTOs;
using TShop.Domain.Entities;
using TShop.Web.ViewModels;
namespace TShop.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BrandDto, BrandVM>().ReverseMap();
            CreateMap<Brand, BrandDto>().ReverseMap();
        }
    }
}
