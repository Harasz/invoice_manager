using System;
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
    public class InvoiceService
    {
        private readonly MainDbContext _dbContext;
        private readonly ProductListService _productListService;
        private readonly ProductService _productService;

        public InvoiceService(MainDbContext dbContext, ProductListService productListService,
            ProductService productService)
        {
            _dbContext = dbContext;
            _productListService = productListService;
            _productService = productService;
        }

        public async Task<List<GetInvoice>> ToDto(IEnumerable<Invoice> invoices)
        {
            var results = await Task.WhenAll(invoices.Select(ToDto));
            return results.OfType<GetInvoice>().ToList();
        }

        public async Task<GetInvoice> ToDto(Invoice invoice)
        {
            var companyId = await _productService.GetOwnerId(invoice.ProductsLists.First().ProductId);
            return new GetInvoice
            {
                Id = invoice.Id,
                Note = invoice.Note,
                Type = invoice.Type,
                PaymentMethod = invoice.PaymentMethod,
                DateDue = invoice.DateDue,
                PaidAt = invoice.PaidAt.Value,
                IssuedAt = invoice.IssuedAt,
                IssuedById = invoice.IssuedById,
                ClientId = invoice.ClientId,
                CompanyId = companyId,
                Products = ProductListService.ToDto(invoice.ProductsLists),
                CreatedAt = invoice.CreatedAt,
                UpdatedAt = invoice.UpdatedAt
            };
        }

        public async Task<List<Invoice>> GetAll()
        {
            return await _dbContext.Invoices
                .Include("ProductsLists")
                .Include("ProductsLists.Product")
                .Include("ProductsLists.Product.Tax")
                .ToListAsync();
        }

        public async Task<Invoice> GetById(int id)
        {
            return await _dbContext.Invoices
                .Include("ProductsLists")
                .Include("ProductsLists.Product")
                .Include("ProductsLists.Product.Tax")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<EntityEntry<Invoice>> Create(PutInvoice invoice)
        {
            try
            {
                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                EntityEntry<Invoice> result;

                result = _dbContext.Invoices.Add(
                    new Invoice
                    {
                        Note = invoice.Note,
                        Type = invoice.Type.Value,
                        PaymentMethod = invoice.PaymentMethod.Value,
                        DateDue = invoice.DateDue.Value,
                        PaidAt = invoice.PaidAt,
                        ClientId = invoice.ClientId.Value,
                        IssuedById = 1
                    });

                _dbContext.SaveChanges();

                var invoiceIssuedCompany = -1;
                if (invoice.Products is not null)
                    foreach (var product in invoice.Products)
                    {
                        var productOwner = await _productService.GetOwnerId(product.ProductId.Value);

                        if (invoiceIssuedCompany == -1) invoiceIssuedCompany = productOwner;

                        if (productOwner != invoiceIssuedCompany) return null;

                        _productListService.Create(new PutProductListFull
                            {Count = product.Count, InvoiceId = result.Entity.Id, ProductId = product.ProductId});
                    }

                _dbContext.SaveChanges();
                scope.Complete();
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
            var invoice = await GetById(id);

            if (invoice is null) return false;

            using var scope = new TransactionScope();
            _productListService.DeleteAllByInvoiceId(invoice.Id);
            _dbContext.Invoices.Remove(invoice);
            _dbContext.SaveChanges();
            scope.Complete();

            return true;
        }

        public async Task<bool> AcceptPayment(Invoice invoice)
        {
            invoice.PaidAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}