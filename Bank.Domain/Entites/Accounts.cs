namespace Bank.Domain.Entites
{
    public class Accounts
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
        public AccountTypes AccountTypes { get; set; }

        public Accounts(int accountId, decimal balance)
        {
            AccountId = accountId;
            Balance = balance;
        }
    }
}
