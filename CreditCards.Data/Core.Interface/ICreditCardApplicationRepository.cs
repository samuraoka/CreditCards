using CreditCards.Core.Model;
using System.Threading.Tasks;

namespace CreditCards.Core.Interface
{
    public interface ICreditCardApplicationRepository
    {
        Task AddAsync(CreditCardApplication application);
    }
}
