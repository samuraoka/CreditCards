using CreditCards.Core.Interface;
using CreditCards.Core.Model;
using System.Threading.Tasks;

namespace CreditCards.Infrastructure
{
    public class EntityFrameworkCreditCardApplicationRepository : ICreditCardApplicationRepository
    {
        private readonly AppDbContext _dbContext;

        public EntityFrameworkCreditCardApplicationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(CreditCardApplication application)
        {
            _dbContext.CreditCardApplication.Add(application);

            return _dbContext.SaveChangesAsync();
        }

        public async Task<CreditCardApplication> FindAsync(int applicationId)
        {
            return await _dbContext.CreditCardApplication.FindAsync(applicationId);
        }
    }
}
