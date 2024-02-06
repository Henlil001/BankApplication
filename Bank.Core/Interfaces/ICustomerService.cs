using Bank.Domain.DTO;
using Bank.Domain.Entites;

namespace Bank.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<(NewCustomerDTO, bool)> CreateCostumer(CreateNewCustomerInput createNewCustomer);
              
    }
}
