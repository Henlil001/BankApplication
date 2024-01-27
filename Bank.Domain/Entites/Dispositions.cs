namespace Bank.Domain.Entites
{
    public class Dispositions
    {
        public int DispositionId { get; set; }
        public List<Customer> Customer { get; set; }
        public List<Accounts> Accounts { get; set; }
        public string Type { get; set; }
    }
}
