using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UserSubscriptionWebApi.Data;
using UserSubscriptionWebApi.Exceptions;
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
            if (prod != null)
            {
                _context.Products.Remove(prod);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Product>> GetALLPaginated(int page, int limit)
        {
            var products = await _context.Products.Include(s => s.Subscriptions).Skip((int)((page - 1) * limit)).Take((int)limit).ToListAsync();
            if (products.Any())
            {
                return products;
            }
            return null;
        }

        public async Task<IEnumerable<Product>> GetALL()
        {
            var products = await _context.Products.Include(s => s.Subscriptions).ToListAsync();
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

        public async Task<Product> Update(int id, ProductRequestDTO requestDTO)
        {
            var prod = await GetById(id);
            if (prod != null)
            {
                prod.Name = requestDTO.Name;
                prod.Description = requestDTO.Description;
                var result = _context.Products.Update(prod);
                return result.Entity;
            }
            return null;

        }
    }
}
