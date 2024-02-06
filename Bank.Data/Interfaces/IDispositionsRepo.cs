
using Bank.Domain.Entites;

namespace Bank.Data.Interfaces
{
    public interface IDispositionsRepo
    {
        List<Dispositions> GetDispositions(int userId);
    }
}
