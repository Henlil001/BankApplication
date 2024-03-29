﻿using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain.DTO;
using Bank.Domain.Entites;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bank.Core.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepo _loginRepo;
        private readonly ICustomerRepo _customerRepo;


        public LoginService(ILoginRepo repo, ICustomerRepo customerRepo)
        {
            _loginRepo = repo;
            _customerRepo = customerRepo;

        }
        public async Task<string> LoginAsync(string username, string password)
        {
            var checkUsername = await _loginRepo.CheckUsernameAsync(username);

            if (checkUsername is null || BCrypt.Net.BCrypt.Verify(password, checkUsername.Password) == false)
                return string.Empty;

            List<Claim> claims = new List<Claim>();

            if (checkUsername.Customer is null)  //Detta sker om det är admin som loggar in
                claims.Add(new Claim(ClaimTypes.NameIdentifier, checkUsername.LoginID.ToString()));

            else
                claims.Add(new Claim(ClaimTypes.NameIdentifier, checkUsername.Customer.CustomerId.ToString()));

            claims.Add(new Claim(ClaimTypes.Role, checkUsername.Role));

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
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;

        }
        public async Task<(NewCustomerDTO, bool)> CreateLoginToExictingCustomer(Login login)
        {
            var newLogin = new NewCustomerDTO();
            if (login.UserName is null || login.Password.Length < 5 || login.Customer is null)
                return (newLogin, false);

            var check = await _loginRepo.CheckUsernameAsync(login.UserName);

            if (check != null)
                return (newLogin, false);

            var allCustomer = await _customerRepo.GetAllCustomersAsync();

            var checkCustomerInput = allCustomer.SingleOrDefault(c => c.GivenName == login.Customer.GivenName &&
                                                           c.SurName == login.Customer.SurName &&
                                                           c.StreetAddress == login.Customer.StreetAddress &&
                                                           c.Birthday == login.Customer.Birthday);
            if (checkCustomerInput is null)
                return (newLogin, false);

            login.Password = BCrypt.Net.BCrypt.HashPassword(login.Password);
            login.Role = "Customer";
            login.Customer.CustomerId = checkCustomerInput.CustomerId;

            newLogin = _loginRepo.CreateLoginToExictingCustomer(login);

            return (newLogin, true);
        }

    }
}
