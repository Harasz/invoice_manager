using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using invoice_manager.DBContexts;
using invoice_manager.Dtos;
using invoice_manager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace invoice_manager.Services
{
    public class ClientService
    {
        private readonly MainDbContext _dbContext;

        public ClientService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static List<Dtos.GetClient> ToDto(IEnumerable<Client> clients)
        {
            return clients.Select(ToDto).ToList();
        }

        public static Dtos.GetClient ToDto(Client client)
        {
            return new()
            {
                Id = client.Id,
                Name = client.Name,
                AddressLine1 = client.AddressLine1,
                AddressLine2 = client.AddressLine2,
                PostalCode = client.PostalCode,
                City = client.City,
                TaxNumber = client.TaxNumber,
                IBAN = client.IBAN,
                CreatedAt = client.CreatedAt,
                UpdatedAt = client.UpdatedAt
            };
        }

        public async Task<List<Client>> GetAll()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task<Client> GetById(int id)
        {
            return await _dbContext.Clients.FindAsync(id);
        }

        public async Task<EntityEntry<Client>> Create(Client client)
        {
            if (!IsValid(client))
            {
                throw new ValidationException();
            }
            var result = await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var client = await GetById(id);

            if (client is null) return false;

            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        
        public async Task<Client> Edit(int id, PutClient newValues)
        {
            var client = await GetById(id);

            if (client is null) return null;

            _dbContext.Entry(client).CurrentValues.SetValues(newValues);
            await _dbContext.SaveChangesAsync();
            return await GetById(id);
        }

        private static bool IsValid(Client client)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(client, null, null);
            return Validator.TryValidateObject(client, context, results, true);
        }
    }
}