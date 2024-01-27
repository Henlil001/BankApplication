namespace Bank.Domain.DTO
{
    public class TransactionsDTO
    {
        public DateTime Date { get; set; }
        public string Operation { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string? Comment { get; set; }
    }
}
