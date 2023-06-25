using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserSubscriptionWebApi.Data;
using UserSubscriptionWebApi.IServices;
using UserSubscriptionWebApi.Models;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SubscriptionService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DateTime ComputeEndDate(int id)
        {
            var subscriptionType = _context.SubscriptionTypes.Where(x => x.SubscriptionTypeId == id).FirstOrDefault();
            return DateTime.UtcNow.AddDays(subscriptionType.Days);
        }

        public async Task<Subscription> Create(SubscriptionRequestDTO requestDTO)
        {
            var new_subscription = _mapper.Map<Subscription>(requestDTO);
            new_subscription.EndDate = ComputeEndDate(new_subscription.SubscriptionTypeId);

            new_subscription.IsActive = new_subscription.StartDate <= DateTime.UtcNow && new_subscription.EndDate >= DateTime.UtcNow;
            _context.Subscriptions.Add(new_subscription);
            var affected_rows = await _context.SaveChangesAsync();
            if (affected_rows > 0)
            {
                await _context.Entry(new_subscription)
                    .Reference(s => s.ApplicationUser)
                    .LoadAsync();

                await _context.Entry(new_subscription)
                    .Reference(s => s.SubscriptionType)
                    .LoadAsync();

                await _context.Entry(new_subscription)
                    .Reference(s => s.Product)
                    .LoadAsync();
                return new_subscription;
            }
            return null;
        }

        public async Task<int> GeTSubscriptionRemainingDays(int subscriptionId)
        {
            var subscription_remaining_days = await _context.SubscriptionDays.FromSqlInterpolated($"SELECT get_subscription_remaining_days({subscriptionId}) AS RemainingDays")
            .FirstOrDefaultAsync();
            if (subscription_remaining_days.remainingdays != null)
            {
                return subscription_remaining_days.remainingdays;
            }
            return 0;
        }

        public async Task<IEnumerable<UserSubscription>> GetSubscriptionsByUser(string userId)
        {
            var user_subscriptions = await _context.UserSubscriptions.FromSqlRaw("Select * from public.get_user_subscriptions({0})", userId).ToListAsync();
            if (user_subscriptions.Any())
            {
                return user_subscriptions;
            }
            return null;
        }
    }
}
