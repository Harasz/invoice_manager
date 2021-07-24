using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using invoice_manager.Dtos;
using invoice_manager.Models;
using invoice_manager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace invoice_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientController : Controller
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetClient>>> GetClients()
        {
            return Json(ClientService.ToDto(await _clientService.GetAll()));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetClient))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _clientService.GetById(id);
            if (client is null) return NotFound();
            return Json(ClientService.ToDto(client));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetClient))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<GetClient>> CreateClient(PutClient putClient)
        {
            if (putClient is null) return BadRequest(new ArgumentNullException());

            EntityEntry<Client> result;

            try
            {
                result = await _clientService.Create(new Client
                {
                    Name = putClient.Name,
                    AddressLine1 = putClient.AddressLine1,
                    AddressLine2 = putClient.AddressLine2,
                    PostalCode = putClient.PostalCode,
                    City = putClient.City,
                    TaxNumber = putClient.TaxNumber,
                    IBAN = putClient.IBAN
                });
            }
            catch (ValidationException error)
            {
                return BadRequest(error);
            }


            if (result is null) return Conflict();

            return CreatedAtAction(nameof(Dtos.GetClient), new {id = result.Entity.Id},
                ClientService.ToDto(result.Entity));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetClient))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetClient>> EditClient(int id, PutClient putClient)
        {
            if (putClient is null) return BadRequest(new ArgumentNullException());

            var result = await _clientService.Edit(id, putClient);
            
            if (result is null) return NotFound();

            return CreatedAtAction(nameof(GetClient), new {id},
                ClientService.ToDto(result));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteClient(int id)
        {
            var result = await _clientService.Delete(id);

            if (result) return Ok();

            return NotFound();
        }
    }
}