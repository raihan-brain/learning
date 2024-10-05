using learning.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace learning.Data
{
    public class BillingContext : DbContext
    {
        private IConfiguration _config;

        public BillingContext(IConfiguration config)
        {
            _config = config;
        }

        public DbSet<Customer> customers => Set<Customer>();
        public DbSet<Employee> employees => Set<Employee>();
        public DbSet<TimeBill> timeBills => Set<TimeBill>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var connectionString = _config["ConnectionStrings:BillingDb"];

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>().HasData(
                new Address { Id = 1, Addresszline1 = "123 Main St", City = "Anytown", StateProvince = "NY", PostalCode = "12345" },
                new Address { Id = 2, Addresszline1 = "1234 Main St", City = "Anytown", StateProvince = "CA", PostalCode = "1345" }
            );

            modelBuilder.Entity<Customer>().HasData(
                new { Id = 1, CompanyName = "ABC Corp", AddressId = 1, Contact = "John Doe", PhoneNumber = "123-456-7890" },
                new { Id = 2, CompanyName = "ACI", AddressId = 2, Contact = "John YEEE", PhoneNumber = "123-456-7890" }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "tee@yopmail.com",
                    BillingRate = 100.00,
                    ImageUrl = "https://via.placeholder.com/150"
                },
                 new Employee
                 {
                     Id = 2,
                     Name = "Bret Lw",
                     Email = "bee@yopmail.com",
                     BillingRate = 100.00,
                     ImageUrl = "https://via.placeholder.com/150"
                 },
                   new Employee
                   {
                       Id = 3,
                       Name = "twitter",
                       Email = "twit@yopmail.com",
                       BillingRate = 100.00,
                       ImageUrl = "https://via.placeholder.com/150"
                   }
            );
        }
    }
}
