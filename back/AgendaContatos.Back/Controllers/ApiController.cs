using AgendaContatos.Back.data;
using AgendaContatos.Back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AgendaContatos.Back.services.Users;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendaContatos.Back.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly INewUserService _newUserService;

        public ApiController(IAuthService authService, INewUserService NewUserService)
        {
            _authService = authService;
            _newUserService = NewUserService;
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Auth([FromBody]User user)
        {
            string authenticatedUser;

            if (user == null) return NotFound(new { message = "Login inválido" });
            
            authenticatedUser = await _authService.AuthUser(user);
             
            if(authenticatedUser.Equals("InvalidUser"))
            {
                return BadRequest(new { message = "Usuário Invalido" });
            }           
            
            return Ok(authenticatedUser);
    
        }

        [HttpGet]
        [Route("auth/verify")]
        [Authorize]
        public IActionResult Verify()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // Verifique se o usuário está autenticado e execute a lógica necessária
            return Ok($"Usuário autenticado: {userId}");
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateNewUser([FromBody]User user)
        {
            if(user != null)
            {
                var createdUser = await _newUserService.CreateNewUser(user);
                    
                if(createdUser.Equals("UserCreated"))
                {
                    return Ok(createdUser);
                }

            }
            return Unauthorized("error");

        }
            
        }
}
