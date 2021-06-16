using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using invoice_manager.DBContexts;
using invoice_manager.Dtos;
using invoice_manager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MySqlConnector;

namespace invoice_manager.Services
{
    public class TaxService
    {
        private readonly MainDbContext _dbContext;
        
        public TaxService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<GetTax> ToDto(List<Tax> taxes)
        {
            return taxes.Select(ToDto).ToList();
        }
        
        public GetTax ToDto(Tax tax)
        {
            return new() {Id = tax.Id, Multiplier = tax.Multiplier};
        }

        public async Task<List<Tax>> GetAll()
        {
            return await _dbContext.Taxes.ToListAsync();
        }
        
        public async Task<Tax> GetById(int id)
        {
            return await _dbContext.Taxes.FindAsync(id);
        }

        public async Task<EntityEntry<Tax>> Create(Tax tax)
        {
            try
            {
                var result = await _dbContext.Taxes.AddAsync(tax);
                await _dbContext.SaveChangesAsync();
                return result;
            } catch (DbUpdateException e)
            {
                if (e.InnerException is MySqlException {ErrorCode: MySqlErrorCode.DuplicateKeyEntry})
                {
                    return null;
                }

                throw;
            }
        }
        
        public async Task<bool> Delete(int id)
        {
            var tax = await GetById(id);

            if (tax is null) return false;
            
            _dbContext.Taxes.Remove(tax);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}