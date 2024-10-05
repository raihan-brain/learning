using learning.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace learning.Data
{
    public class BillingRepository : IBillingRepository
    {
        private BillingContext _context;

        public BillingRepository(BillingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.employees
                .OrderBy(e => e.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _context.customers
                .OrderBy(c => c.CompanyName)
                .ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
