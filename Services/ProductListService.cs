using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using invoice_manager.DBContexts;
using invoice_manager.Dtos;
using invoice_manager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MySqlConnector;

namespace invoice_manager.Services
{
    public class ProductListService
    {
        private readonly MainDbContext _dbContext;

        public ProductListService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static List<GetProductList> ToDto(IEnumerable<ProductsList> productsLists)
        {
            return productsLists.Select(ToDto).ToList();
        }

        public static GetProductList ToDto(ProductsList productsLists)
        {
            return new()
            {
                Id = productsLists.Id,
                Count = productsLists.Count,
                InvoiceId = productsLists.InvoiceId,
                Product = ProductService.ToDto(productsLists.Product),
                CreatedAt = productsLists.CreatedAt,
                UpdatedAt = productsLists.UpdatedAt
            };
        }

        public async Task<List<ProductsList>> GetAll()
        {
            return await _dbContext.ProductsLists
                .Include("Product")
                .Include("Product.Tax")
                .ToListAsync();
        }
        
        public List<ProductsList> GetAllByInvoiceId(int invoiceId)
        {
            return _dbContext.ProductsLists.Where(list => list.InvoiceId == invoiceId).ToList();
        }

        public async Task<ProductsList> GetById(int id)
        {
            return await _dbContext.ProductsLists
                .Include("Product")
                .Include("Product.Tax")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public EntityEntry<ProductsList> Create(PutProductListFull productList)
        {
            try
            {
                var result = _dbContext.ProductsLists.Add(
                    new ProductsList
                    {
                        Count = productList.Count.Value,
                        InvoiceId = productList.InvoiceId.Value,
                        ProductId = productList.ProductId.Value,
                    });
                
                return result;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is MySqlException {ErrorCode: MySqlErrorCode.DuplicateKeyEntry}) return null;

                throw;
            }
        }
        
        public async Task<EntityEntry<ProductsList>> CreateAsync(PutProductListFull productList)
        {
            try
            {
                var result = await _dbContext.ProductsLists.AddAsync(
                    new ProductsList
                    {
                        Count = productList.Count.Value,
                        InvoiceId = productList.InvoiceId.Value,
                        ProductId = productList.ProductId.Value,
                    });

                await _dbContext.SaveChangesAsync();
                return result;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is MySqlException {ErrorCode: MySqlErrorCode.DuplicateKeyEntry}) return null;

                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var productsList = await GetById(id);

            if (productsList is null) return false;

            return await Delete(productsList);
        }
        
        public async Task<bool> Delete(ProductsList productsList)
        {
            _dbContext.ProductsLists.Remove(productsList);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public void DeleteAllByInvoiceId(int invoiceId)
        {
            var productsLists = GetAllByInvoiceId(invoiceId);
            _dbContext.ProductsLists.RemoveRange(productsLists);
        }
    }
}