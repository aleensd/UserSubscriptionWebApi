using AutoMapper;
using Microsoft.Extensions.Options;
using UserSubscriptionWebApi.Data;
using UserSubscriptionWebApi.Models;
using UserSubscriptionWebApi.Models.DTOs;


namespace UserSubscriptionWebApi.Profiles
{
    public class SubscribtionProfile : Profile
    {
        public SubscribtionProfile()
        {
            CreateMap<SubscriptionRequestDTO, Subscription>()
               .ForMember(
               dest => dest.ApplicationUserId,
               opt => opt.MapFrom(src => src.ApplicationUserId))
                 .ForMember(
               dest => dest.ProductId,
               opt => opt.MapFrom(src => src.ProductId))
                .ForMember(
               dest => dest.SubscriptionTypeId,
               opt => opt.MapFrom(src => src.SubscriptionTypeId))
                .ForMember(
               dest => dest.StartDate,
               opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
