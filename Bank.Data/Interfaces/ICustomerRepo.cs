using Bank.Domain.DTO;
using Bank.Domain.Entites;

namespace Bank.Data.Interfaces
{
    public interface ICustomerRepo
    {
        Task<List<Customer>> GetAllCustomersAsync();
        NewCustomerDTO CreateCustomer(CreateNewCustomerInput createNewCustomer);

    }
}
