using AutoMapper;
using System.Collections;
using UserSubscriptionWebApi.Data;
using UserSubscriptionWebApi.IServices;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.Services
{
    public class SubscriptionTypeService : ISubscriptionType
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SubscriptionTypeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<SubscriptionType> Create(SubscriptionTypeRequestDTO requestDTO)
        {
            var new_subscriptionType = _mapper.Map<SubscriptionType>(requestDTO);
            _context.SubscriptionTypes.Add(new_subscriptionType);
            var affected_rows = await _context.SaveChangesAsync();
            if (affected_rows > 0)
            {
                return new_subscriptionType;

            }
            return null;
        }

        public IEnumerable<SubscriptionType> GetALL()
        {
            var subscriptionTypes = _context.SubscriptionTypes.ToList();
            if (subscriptionTypes.Any())
            {

                return subscriptionTypes;
            }
            return null;
        }
    }
}
