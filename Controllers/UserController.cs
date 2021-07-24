using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dtos.GetUser>>> GetUsers()
        {
            return Json(UserService.ToDto(await _userService.GetAll()));
        }
        
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dtos.GetUser))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dtos.GetUser>> GetUser(int id)
        {
            var user = await _userService.GetById(id);
            if (user is null) return NotFound();
            return Json(UserService.ToDto(user));
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Dtos.GetUser))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<User>> CreateUser(Dtos.PutUser putUser)
        {
            if (putUser is null) return BadRequest(new ArgumentNullException());

            var result = await _userService.Create(putUser);

            if (result is null)
            {
                return Conflict();
            }

            return CreatedAtAction(nameof(GetUser), new { id = result.Entity.Id }, UserService.ToDto(result.Entity));
        }
        
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetById(id);

            if (user.Role is UserRoles.Admin && await _userService.GetOverallUserByRole(UserRoles.Admin) == 1)
            {
                return StatusCode(403, "Can not delete last user with Admin role.");
            }

            var result = await _userService.Delete(id);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }
        
        [HttpPost("makeAdmin/{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> MakeAdmin(int id)
        {
            var user = await _userService.GetById(id);

            if (user is null)
            {
                return NotFound();
            }

            var result = await _userService.MakeAdmin(user);

            return result ? Ok() : new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}