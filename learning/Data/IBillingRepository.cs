using learning.Data.Entities;

namespace learning.Data
{
    public interface IBillingRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<IEnumerable<Customer>> GetCustomersWithAddress();
        Task<IEnumerable<Employee>> GetEmployees();
        void AddEntity<T>(T cus) where T : notnull;
        Task<bool> SaveChanges();
        Task<Customer?> GetCustomer(int id);

    }
}