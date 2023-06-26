using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserSubscriptionWebApi.Data;
using UserSubscriptionWebApi.IServices;
using UserSubscriptionWebApi.Models;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Product> Create(ProductRequestDTO requestDTO)
        {
            var new_product = _mapper.Map<Product>(requestDTO);
            _context.Products.Add(new_product);
            var affected_rows = await _context.SaveChangesAsync();
            if (affected_rows > 0)
            {
                return new_product;

            }
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var prod = await GetById(id);
            _context.Products.Remove(prod);
            await _context.SaveChangesAsync();
            return prod != null ? true : false;
        }

        public async Task<IEnumerable<Product>> GetALL()
        {
            var products = await _context.Products.ToListAsync();
            if (products.Any())
            {
                return products;
            }
            return null;
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }
    }
}
