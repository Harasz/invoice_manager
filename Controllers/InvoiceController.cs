using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using invoice_manager.Dtos;
using invoice_manager.Models;
using invoice_manager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace invoice_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly InvoiceService _invoiceService;
        private readonly ProductListService _productListService;
        private readonly ProductService _productService;

        public InvoiceController(InvoiceService invoiceService, ProductListService productListService, ProductService productService)
        {
            _invoiceService = invoiceService;
            _productListService = productListService;
            _productService = productService;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetInvoice))]
        public async Task<ActionResult<IEnumerable<GetInvoice>>> GetInvoices()
        {
            return Json(await _invoiceService.ToDto(await _invoiceService.GetAll()));
        }
        
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetInvoice))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetUser>> GetInvoice(int id)
        {
            var invoice = await _invoiceService.GetById(id);
            if (invoice is null) return NotFound();
            return Json(await _invoiceService.ToDto(invoice));
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetInvoice))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Invoice>> CreateInvoice(PutInvoice putInvoice)
        {
            if (putInvoice is null) return BadRequest(new ArgumentNullException());

            var result = await _invoiceService.Create(putInvoice);

            if (result is null)
            {
                return Conflict();
            }

            return CreatedAtAction(nameof(GetInvoice), new { id = result.Entity.Id }, await _invoiceService.GetById(result.Entity.Id));
        }
        
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeleteInvoice(int id)
        {
            var result = await _invoiceService.Delete(id);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }
        
        [HttpPost("{id:int}/addProduct")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> AddProduct(int id, PutProductList product)
        {
            var invoice = await _invoiceService.GetById(id);

            if (invoice.ProductsLists.Count > 0)
            {
                var sampleProduct = invoice.ProductsLists.First();
                var productOwner = await _productService.GetOwnerId(product.ProductId.Value);
                if (sampleProduct.Product.OwnerId != productOwner)
                {
                    return StatusCode(403, "Product owner not match.s");
                }
            }
            
            await _productListService.CreateAsync(new PutProductListFull {Count = product.Count, InvoiceId = id, ProductId = product.ProductId});

            return Ok();
        }
        
        [HttpDelete("{id:int}/removeProduct/{listId:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RemoveProduct(int id, int listId)
        {
            var product = await _productListService.GetById(listId);

            if (product is null || id != product.InvoiceId) return NotFound();

            await _productListService.Delete(listId);

            return Ok();
        }
        
        [HttpPost("{id:int}/acceptPayment")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> AcceptPayment(int id)
        {
            var invoice = await _invoiceService.GetById(id);

            if (invoice is null) return NotFound();
            if (invoice.PaidAt is not null) return StatusCode(403, "Invoice has already been paid.");

            await _invoiceService.AcceptPayment(invoice);

            return Ok();
        }
    }
}