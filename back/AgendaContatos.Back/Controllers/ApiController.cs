﻿using AgendaContatos.Back.Models;
using Microsoft.AspNetCore.Mvc;
using AgendaContatos.Back.services.Users;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using AgendaContatos.Back.services.Contacts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendaContatos.Back.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IContactsService _contactsService;

        public ApiController(IAuthService authService, IUserService UserService, IContactsService contactsService)
        {
            _authService = authService;
            _userService = UserService;
            _contactsService = contactsService;
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Auth([FromBody]User user)
        {
            string authenticatedUser;

            if (user == null) return NotFound(new { message = "Login inválido" });
            
            authenticatedUser = await _authService.AuthUser(user);
             
            if(authenticatedUser.Equals("InvalidLogin")) return Unauthorized(new { message = "Incorrect user or password" });
                       
            
            return Ok(authenticatedUser);
    
        }

        [HttpGet]
        [Route("auth/verify")]
        [Authorize]
        public IActionResult Verify()
        {
            var tokenParams = GetJwtParemeters();

            return Ok($"Usuário autenticado {tokenParams.userName}");
        }

        [Authorize]
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateNewUser([FromBody]User user)
        {
            if(user != null)
            {
                var createdUser = await _userService.CreateNewUser(user);
                    
                if(createdUser.Equals("UserCreated")) return Ok(createdUser);
                

            }
            return BadRequest("Error on user creation");

        }

        [Authorize]
        [HttpPost("create-contact")]
        public async Task<IActionResult> CreateNewContact([FromForm] Contact contact)
        {
            if (contact != null)
            {
                var newContact = await _contactsService.CreateContact(contact, GetJwtParemeters().id);
                
                if (newContact.Equals("ContactCreated")) return Ok(newContact);
                
            }
            return Unauthorized("error");
        }

        [Authorize]
        [HttpGet("get-user-contacts")]        
        public async Task<IActionResult> GetUserContacts()
        {
            var userId = GetJwtParemeters().id;

            if (!userId.Equals(null))
            {
                var userContacts = await _contactsService.GetUserContacts(userId);
                if (userContacts.Count >= 0) return Ok(userContacts);
                
            }
            return Unauthorized("error");
        }

        [Authorize]
        [HttpDelete("delete-contact")]
        public async Task<IActionResult> DeleteContact([FromQuery] string contactId, [FromQuery] string userId)
        {
            try
            {
               await _contactsService.DeleteContact(contactId, userId);
                return Ok("UserDeleted");
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("update-contact")]
        public async Task<IActionResult> UpdateContact([FromForm] Contact contact)
        {
            try
            {
                await _contactsService.UpdateContact(contact);
                return Ok("ContactUpdated");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private dynamic GetJwtParemeters()
        {
            string authorization = HttpContext.Request.Headers.Authorization;

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(authorization.Replace("Bearer ", "")).Claims.ToList();

            var tokenParams = new 
            { 
                userName = token[0].Value, 
                id = token[1].Value,
                email = token[2].Value, 
            };

            return tokenParams;
        }


    }
}
