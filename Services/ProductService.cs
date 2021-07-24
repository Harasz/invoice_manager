using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using invoice_manager.DBContexts;
using invoice_manager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace invoice_manager.Services
{
    public class ProductService
    {
        private readonly MainDbContext _dbContext;
        
        public ProductService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static Dtos.GetProduct ToDto(Product product)
        {
            return new()
            {
                Id = product.Id,
                Name = product.Name,
                Unit = product.Unit,
                PricePerUnit = product.PricePerUnit,
                Tax = TaxService.ToDto(product.Tax),
                OwnerId = product.OwnerId,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
            };
        }
        
        public static List<Dtos.GetProduct> ToDto(List<Product> products)
        {
            return products.Select(ToDto).ToList();
        }
        
        public async Task<List<Product>> GetAll()
        {
            return await _dbContext.Products.Include(product => product.Tax).ToListAsync();
        }
        
        public async Task<Product> GetById(int id)
        {
            return await _dbContext.Products.Include(product => product.Tax).FirstAsync(product => product.Id == id);
        }
        
        public async Task<List<Product>> GetByCompanyId(int companyId)
        {
            return await _dbContext.Products.Where(product => product.OwnerId == companyId).ToListAsync();
        }

        public async Task<EntityEntry<Product>> Create(Product tax)
        {
            var result = await _dbContext.Products.AddAsync(tax);
            await _dbContext.SaveChangesAsync();
            return result;
        }
        
        public async Task<bool> Delete(int id)
        {
            var product = await GetById(id);

            if (product is null) return false;
            
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        
        public async Task<int> GetOwnerId(int id)
        {
            var product = await GetById(id);
            return product.OwnerId;
        }
    }
}