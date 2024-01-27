using AutoMapper;
using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain.DTO;

namespace Bank.Core.Service
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepo _accountsRepo;
        private readonly IMapper _mapper;
        public AccountsService(IAccountsRepo accountsRepo, IMapper mapper)
        {
            _accountsRepo = accountsRepo;
            _mapper = mapper;
        }
        public List<AccountsDTO> ShowAccounts(int customerId)
        {
            var accounts = _accountsRepo.ShowAccounts(customerId);

            var accountsDTO = new List<AccountsDTO>();

            foreach (var account in accounts)
            {
                var mapedaAccounts = new AccountsDTO
                {
                    AccountId = account.AccountId,
                    AccountType = account.AccountTypes.TypeName,
                    Balance = account.Balance,

                };
                //var mapedaAccounts = _mapper.Map<AccountsDTO>(account);
                accountsDTO.Add(mapedaAccounts);
            }
            return accountsDTO;
        }
        public int CreateNewAccount(CreateAccountDTO account, int customerId)
        {
            if (account.Frequency is null || !(account.AccountType1ForStandardPrivat2ForSavings == 1 || account.AccountType1ForStandardPrivat2ForSavings == 2))
                return 0;

            return _accountsRepo.CreateNewAccount(account, customerId);
        }


    }
}
