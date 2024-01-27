using Bank.Domain.DTO;
using Bank.Domain.Entites;

namespace Bank.Core.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();

        Task<(NewCustomerDTO, bool)> CreateCostumerAsync(CreateNewCustomerInput createNewCustomer);

        


    }
}
