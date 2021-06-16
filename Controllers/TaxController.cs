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
    public class TaxController : Controller
    {
        private readonly TaxService _taxService;

        public TaxController(TaxService taxService)
        {
            _taxService = taxService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dtos.GetTax>>> GetTaxes()
        {
            return Json(_taxService.ToDto(await _taxService.GetAll()));
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Dtos.GetTax>> GetTax(int id)
        {
            return Json(_taxService.ToDto(await _taxService.GetById(id)));
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Dtos.GetTax))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Tax>> CreateTax(Dtos.PutTax putTax)
        {
            if (putTax is null) return BadRequest(new ArgumentNullException());
            
            var result = await _taxService.Create(new Tax {Multiplier = putTax.Multiplier});

            if (result is null)
            {
                return Conflict();
            }

            return CreatedAtAction(nameof(GetTax), new { id = result.Entity.Id }, _taxService.ToDto(result.Entity));
        }
        
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteTax(int id)
        {
            var result = await _taxService.Delete(id);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}