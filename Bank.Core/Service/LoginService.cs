using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain.DTO;
using Bank.Domain.Entites;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepo _repo;

        public LoginService(ILoginRepo repo)
        {
            _repo = repo;
        }
        public LoginToken LoginAdmin(Login login)
        {
            var loginToken = new LoginToken();

            var logedInAdmin = _repo.GetLogin(login);

            if (logedInAdmin == null || logedInAdmin.Role != "Admin")
                return loginToken;

            if (logedInAdmin != null)
                logedInAdmin.Role = char.ToUpper(logedInAdmin.Role[0]) + logedInAdmin.Role.Substring(1).ToLower();

            return CreateToken(login);
        }

        public LoginToken LoginCustomer(Login login)
        {
            var loginToken = new LoginToken();

            var logedInCustomer = _repo.GetLogin(login);

            if (logedInCustomer == null || logedInCustomer.Role != "Customer")
                return loginToken;

            if (logedInCustomer != null)
                logedInCustomer.Role = char.ToUpper(logedInCustomer.Role[0]) + logedInCustomer.Role.Substring(1).ToLower();

            return CreateToken(login);
        }

        private static LoginToken CreateToken(Login logedInUser)
        {
            var loginToken = new LoginToken();
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, logedInUser.Customer.CustomerId.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, logedInUser.Role));

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
            loginToken.Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return loginToken;
        }


    }
}
