using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using invoice_manager.Models;
using invoice_manager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace invoice_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Json(await _productService.GetAll());
        }
        
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetById(id);
            if (product is null) return NotFound();
            return Json(product);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Product>> CreateProduct(Dtos.PutProduct putProduct)
        {
            if (putProduct is null) return BadRequest(new ArgumentNullException());
            
            var result = await _productService.Create(new Product
            {
                Name = putProduct.Name,
                Unit = putProduct.Unit,
                PricePerUnit = putProduct.PricePerUnit,
                TaxId = putProduct.TaxId,
                OwnerId = putProduct.OwnerId
            });

            if (result is null)
            {
                return Conflict();
            }

            return CreatedAtAction(nameof(GetProduct), new { id = result.Entity.Id }, result.Entity);
        }
        
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var result = await _productService.Delete(id);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}