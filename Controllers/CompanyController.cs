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
    public class CompanyController : Controller
    {
        private readonly CompanyService _companyService;
        private readonly ProductService _productService;

        public CompanyController(CompanyService companyService, ProductService productService)
        {
            _companyService = companyService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dtos.GetCompany>>> GetCompanies()
        {
            return Json(await _companyService.GetAll());
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dtos.GetCompany))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _companyService.GetById(id);
            if (company is null) return NotFound();
            return Json(CompanyService.ToDto(company));
        }
        
        [HttpGet("{id:int}/products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dtos.GetProduct))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Company>> GetCompanyProducts(int id)
        {
            var company = await _companyService.GetById(id);
            if (company is null) return NotFound();
            return Json(ProductService.ToDto(await _productService.GetByCompanyId(id)));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Company))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Dtos.GetCompany>> CreateCompany(Dtos.PutCompany putCompany)
        {
            if (putCompany is null) return BadRequest(new ArgumentNullException());

            var result = await _companyService.Create(new Company
            {
                Name = putCompany.Name,
                AddressLine1 = putCompany.AddressLine1,
                AddressLine2 = putCompany.AddressLine2,
                PostalCode = putCompany.PostalCode,
                City = putCompany.City,
                TaxNumber = putCompany.TaxNumber,
                IBAN = putCompany.IBAN,
                PhoneNumber = putCompany.PhoneNumber,
                Email = putCompany.Email,
                Website = putCompany.Website,
                LogoSourcePath = ""
            });

            if (result is null) return Conflict();

            return CreatedAtAction(nameof(GetCompany), new {id = result.Entity.Id}, CompanyService.ToDto(result.Entity));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteCompany(int id)
        {
            var result = await _companyService.Delete(id);

            if (result) return Ok();

            return NotFound();
        }
    }
}