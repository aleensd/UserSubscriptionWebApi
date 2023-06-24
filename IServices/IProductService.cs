using UserSubscriptionWebApi.Models;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.IServices
{
    public interface IProductService
    {
        Task<Product> Create(ProductRequestDTO requestDTO);
        Task<IEnumerable<Product>> GetALL();
    }
}
