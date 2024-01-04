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
                var logedInCustomer = _Service.Login(login);

                if (logedInCustomer == null || logedInCustomer.Role != "Customer")
                    return BadRequest("Invalied Login/You are not a customer");

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, logedInCustomer.Customer.CustomerId.ToString()));
                claims.Add(new Claim(ClaimTypes.Role, logedInCustomer.Role));

                //Sätta upp kryptering. Samma säkerhetsnyckel som när vi satte upp tjänsten
                //Denna förvaras på ett säkert ställe tex Azure Keyvault eller liknande och hårdkodas
                //inte in på detta sätt
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecretKey12345!#kjbgfoilkjgtiyduglih7gtl8gt5"));

                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                //Skapa options för att sätta upp en token
                var tokenOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5142",
                        audience: "http://localhost:5142",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signinCredentials);

                //Generar en ny token som skall skickas tillbaka 
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new { Token = tokenString });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Route("Admin/Login")]
        [HttpPost]
        public IActionResult AdminLogin(Login login)
        {
            try
            {
                var logedInAdmin = _Service.Login(login);

                if (logedInAdmin == null || logedInAdmin.Role != "Admin")
                    return BadRequest("Invalied Login/You are not autherazed as an admin");

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, logedInAdmin.LoginID.ToString()));
                claims.Add(new Claim(ClaimTypes.Role, logedInAdmin.Role));

                //Sätta upp kryptering. Samma säkerhetsnyckel som när vi satte upp tjänsten
                //Denna förvaras på ett säkert ställe tex Azure Keyvault eller liknande och hårdkodas
                //inte in på detta sätt
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecretKey12345!#kjbgfoilkjgtiyduglih7gtl8gt5"));

                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                //Skapa options för att sätta upp en token
                var tokenOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5142",
                        audience: "http://localhost:5142",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signinCredentials);

                //Generar en ny token som skall skickas tillbaka 
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new { Token = tokenString });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
