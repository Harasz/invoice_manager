using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using invoice_manager.DBContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace invoice_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Tax : Controller
    {
        private readonly MainDbContext _dbContext;

        public Tax(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dtos.GetTax>>> GetTaxes()
        {
            var listOfTaxes = await _dbContext.Taxes.ToListAsync();
            return Json(listOfTaxes.Select(tax => new {tax.Id, tax.Multiplier}));
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Dtos.GetTax>> GetTax(int id)
        {
            var tax = await _dbContext.Taxes.FindAsync(id);
            var taxDto = new {tax.Id, tax.Multiplier};
            return Json(taxDto);
        }
    }
}