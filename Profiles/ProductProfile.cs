using AutoMapper;
using UserSubscriptionWebApi.Models;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductRequestDTO, Product>()
                .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
                  .ForMember(
                dest => dest.Description,
                opt => opt.MapFrom(src => src.Description));
        }
    }
}
