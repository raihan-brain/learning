using learning.Data.Entities;

namespace learning.Data
{
    public interface IBillingRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<IEnumerable<Employee>> GetEmployees();
        Task<bool> SaveChanges();
    }
}