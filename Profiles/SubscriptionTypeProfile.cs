using AutoMapper;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.Profiles
{
    public class SubscriptionTypeProfile : Profile
    {
        public SubscriptionTypeProfile()
        {
            CreateMap<SubscriptionTypeRequestDTO, SubscriptionType>()
                .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
                  .ForMember(
                dest => dest.Description,
                opt => opt.MapFrom(src => src.Description))
                 .ForMember(
                dest => dest.Price,
                opt => opt.MapFrom(src => src.Price));
        }
    }
}
