namespace Bank.Domain.Entites
{
    public class Loans
    {
        public int loanId { get; set; }
        public Accounts Accounts { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public decimal Payments { get; set; }
        public string Status { get; set; }
    }
}
