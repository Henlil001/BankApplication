using Bank.Core.Interfaces;
using Bank.Domain.Entites;
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

        [Route("Customer/Login")]
        [HttpPost]
        public IActionResult CustomerLogin(Login login)
        {
            try
            {
                var token = _Service.LoginCustomer(login);

                if (token.Token is null)
                    return BadRequest("Invalied Login / You are not a customer");

                return Ok(token.Token);
                
            }
            catch (Exception)
            {
                return StatusCode(500, "Error, CustomerLogin");
                throw;
            }
        }
        [Route("Admin/Login")]
        [HttpPost]
        public IActionResult AdminLogin(Login login)
        {
            try
            {
                var token = _Service.LoginAdmin(login);

                if(token.Token is null)
                    return BadRequest("Invalied Login / You are not a Admin");

                return Ok(token.Token);
                
            }
            catch (Exception)
            {
                return StatusCode(500, "Error, AdminLogin");
                throw;
            }
        }
    }
}
