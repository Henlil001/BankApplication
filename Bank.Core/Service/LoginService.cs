using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public Login? Login(Login login)
        {
            var logedInUser = _repo.GetLogin(login);

            if (logedInUser != null)
                logedInUser.Role = char.ToUpper(logedInUser.Role[0]) + logedInUser.Role.Substring(1).ToLower();

            return logedInUser;
        }


    }
}
