using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using invoice_manager.DBContexts;
using invoice_manager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace invoice_manager.Services
{
    public class CompanyService
    {
        private readonly MainDbContext _dbContext;

        public CompanyService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static List<Dtos.GetCompany> ToDto(IEnumerable<Company> companies)
        {
            return companies.Select(ToDto).ToList();
        }

        public static Dtos.GetCompany ToDto(Company company)
        {
            return new()
            {
                Id = company.Id,
                Name = company.Name,
                AddressLine1 = company.AddressLine1,
                AddressLine2 = company.AddressLine2,
                PostalCode = company.PostalCode,
                City = company.City,
                TaxNumber = company.TaxNumber,
                IBAN = company.IBAN,
                PhoneNumber = company.PhoneNumber,
                Email = company.Email,
                Website = company.Website,
                LogoSourcePath = company.LogoSourcePath,
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt
            };
        }

        public async Task<List<Company>> GetAll()
        {
            return await _dbContext.Companies.ToListAsync();
        }

        public async Task<Company> GetById(int id)
        {
            return await _dbContext.Companies.FindAsync(id);
        }

        public async Task<EntityEntry<Company>> Create(Company company)
        {
            var result = await _dbContext.Companies.AddAsync(company);
            await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var company = await GetById(id);

            if (company is null) return false;

            _dbContext.Companies.Remove(company);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}