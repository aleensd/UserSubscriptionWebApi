using UserSubscriptionWebApi.Models;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.IServices
{
    public interface ISubscriptionService
    {
        Task<Subscription> Create(SubscriptionRequestDTO requestDTO);
        Task<IEnumerable<UserSubscription>> GetSubscriptionsByUser(string userId);

        Task<int> GeTSubscriptionRemainingDays(int subscriptionId);
    }
}
