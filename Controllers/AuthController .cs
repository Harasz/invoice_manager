using System.Collections.Generic;
using System.Threading.Tasks;
using invoice_manager.Dtos;
using invoice_manager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace invoice_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;
        
        public AuthController(UserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetToken))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<GetToken>>> Login(PutCredentials credentials)
        {
            var user = await _userService.GetByEmail(credentials.Email);

            if (user is null)
            {
                return Forbid();
            }
            
            if (!UserService.VerifyPassword(credentials.Password, user.PasswordHash))
            {
                return Forbid();
            }

            return Json(new GetToken {Token = _jwtService.Generate(user)});
        }
    }
}