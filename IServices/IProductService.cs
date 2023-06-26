using UserSubscriptionWebApi.Models;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.IServices
{
    public interface IProductService
    {
        Task<Product> Create(ProductRequestDTO requestDTO);
        Task<IEnumerable<Product>> GetALLPaginated(int page, int limit);
        Task<IEnumerable<Product>> GetALL();
        Task<Product> GetById(int id);
        Task<bool> Delete(int id);
    }
}
