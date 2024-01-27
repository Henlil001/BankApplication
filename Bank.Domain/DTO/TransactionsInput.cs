namespace Bank.Domain.DTO
{
    public class TransactionsInput
    {
        public int AccountToTransferTo { get; set; }
        public int AccountToTransferFrom { get; set; }
        public decimal Amount { get; set; }

        public TransactionsInput(int accountToTransferTo, int accountToTransferFrom, decimal amount)
        {
            AccountToTransferTo = accountToTransferTo;
            AccountToTransferFrom = accountToTransferFrom;
            Amount = amount;
        }
        public TransactionsInput()
        {
            
        }
    }
}
