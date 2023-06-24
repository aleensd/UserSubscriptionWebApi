using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.IServices
{
    public interface ISubscriptionType
    {
        Task<SubscriptionType> Create(SubscriptionTypeRequestDTO requestDTO);
        Task<IEnumerable<SubscriptionType>> GetALL();
    }
}
