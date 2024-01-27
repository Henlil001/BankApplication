using Bank.Domain.DTO;
using Bank.Domain.Entites;

namespace Bank.Data.Interfaces
{
    public interface ICustomerRepo
    {
        List<Customer> GetAllCustomers();
        Task<NewCustomerDTO> CreateCustomerAsync(CreateNewCustomerInput createNewCustomer);

    }
}
