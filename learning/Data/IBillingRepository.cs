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
        Task<TimeBill?> GetTimeBill(int id);
        Task<IEnumerable<TimeBill>> GetTimeBillsForCustomer(int id);
        Task<TimeBill> GetTimeBillForCustomer(int id, int billId);
    }
} 