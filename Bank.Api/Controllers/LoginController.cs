using Bank.Core.Interfaces;
using Bank.Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _Service;

        public LoginController(ILoginService service)
        {
            _Service = service;
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            try
            {
                string token = _Service.Login(username, password).Result;

                if (token.IsNullOrEmpty())
                    return NotFound("Invalied login");

                return Ok(new { Token = token });

            }
            catch (Exception)
            {
                return StatusCode(500, "Error, Login");
                throw;
            }
        }
        [Route("Admin_Create_Login_To_Exicting_Customer")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateLoginToExictingCustomer(Login login)
        {
            try
            {
                var newLogin = _Service.CreateLoginToExictingCustomer(login);

                if (newLogin.CorrectInput is false)
                    return BadRequest("Password need to be atleast 6 char long / or Invalied Username");

                return Ok(newLogin);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error, CreateLoginToExictingCustomer");
                throw;
            }
        }
    }
}
