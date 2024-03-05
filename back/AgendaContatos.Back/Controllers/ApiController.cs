using AgendaContatos.Back.data;
using AgendaContatos.Back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AgendaContatos.Back.services.Users;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
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
             
            if(authenticatedUser.Equals("InvalidLogin"))
            {
                return Unauthorized(new { message = "Usuário ou senha incorretos." });
            }           
            
            return Ok(authenticatedUser);
    
        }

        [HttpGet]
        [Route("auth/verify")]
        [Authorize]
        public IActionResult Verify()
        {
            string authorization = HttpContext.Request.Headers.Authorization;

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(authorization.Replace("Bearer ", ""));
            var user = token.Claims.First();

            return Ok($"Usuário autenticado: {user.Value}");
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
