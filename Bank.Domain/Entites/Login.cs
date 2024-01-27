namespace Bank.Domain.Entites
{
    public class Login
    {
        public int LoginID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Customer? Customer { get; set; }
    }
}
