using AutoMapper;
using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain.DTO;


namespace Bank.Core.Service
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsRepo _transactionsRepo;
        private readonly IAccountsRepo _accountsRepo;
        private readonly IMapper _mapper;
        private readonly IDispositionsRepo _dispositionsRepo;
        public TransactionsService(ITransactionsRepo transactionsRepo, IMapper mapper, IAccountsRepo accountsRepo, IDispositionsRepo dispositionsRepo)
        {
            _transactionsRepo = transactionsRepo;
            _mapper = mapper;
            _accountsRepo = accountsRepo;
            _dispositionsRepo = dispositionsRepo;
        }
        public async Task<(List<TransactionsDTO>,bool)> ShowTransactionsAsync(int accountId, int id)
        {
            var dispositions = _dispositionsRepo.GetDispositions(id).Where(d => d.AccountId == accountId).ToList();

            if (!dispositions.Any())
                return (new List<TransactionsDTO>(), false);

            

            var transactions = await _transactionsRepo.ShowTransactionsAsync(accountId);

            var transactionsDTO = new List<TransactionsDTO>();

            foreach (var transaction in transactions)
            {
                var mapedTransactions = new TransactionsDTO
                {
                    Date = transaction.Date,
                    Operation = transaction.Operation,
                    Amount = transaction.Amount,
                    Balance = transaction.Balance,
                    Comment = transaction.Symbol

                };
                //var mappedTransactions = _mapper.Map<TransactionsDTO>(transaction);
                transactionsDTO.Add(mapedTransactions);
            }
            return (transactionsDTO, true);
        }

        public bool TransferMoney(TransactionsInput transactions)
        {
            try
            {
                if (transactions.AccountToTransferFrom.ToString() is null ||
                    transactions.AccountToTransferTo.ToString() is null ||
                    transactions.Amount.ToString() is null)
                    return false;

                var accountFrom = _accountsRepo.GetAccount(transactions.AccountToTransferFrom);
                var accountTo = _accountsRepo.GetAccount(transactions.AccountToTransferTo);

                if (accountFrom == null ||accountTo == null || accountFrom.Balance < transactions.Amount) 
                    return false;

                _transactionsRepo.TransferMoney(transactions);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
