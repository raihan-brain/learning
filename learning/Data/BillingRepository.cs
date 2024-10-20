﻿using learning.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace learning.Data
{
    public class BillingRepository : IBillingRepository
    {
        private BillingContext _context;
        private readonly ILogger<IBillingRepository> _logger;

        public BillingRepository(BillingContext context, ILogger<IBillingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                return await _context.employees
                        .OrderBy(e => e.Name)
                        .ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"could not get employees: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            try
            {
                return await _context.customers
                      .OrderBy(c => c.CompanyName)
                      .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"could not get customerts: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> SaveChanges()
        {
            try
            {
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"could not save to the Database: {ex.Message}");
                throw;
            }
        }

        public async Task<Customer?> GetCustomer(int id)
        {
            try
            {
                return await _context.customers
                       .Where(c => c.Id == id)
                       .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"could not get customer: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomersWithAddress()
        {
            try
            {
                return await _context.customers
                    .Include(c => c.Address)
                     .OrderBy(c => c.CompanyName)
                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"could not get customerts: {ex.Message}");
                throw;
            }
        }

        public void AddEntity<T>(T cus) where T : notnull
        {
            _context.Add(cus);
        }

        public async Task<TimeBill?> GetTimeBill(int id)
        {
            var bill = await _context.timeBills
                .Include(b => b.Employee)
                .Include(b => b.Customer)
                .ThenInclude(c => c!.Address)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
            return bill;
        }

        public async Task<IEnumerable<TimeBill>> GetTimeBillsForCustomer(int id)
        {
            return await _context.timeBills
                .Where(b =>b.CustomerId != null && b.Customer.Id == id)
                .Include(b => b.Employee)
                .Include(b => b.Customer)
                .ToListAsync();
        }

        public async Task<TimeBill> GetTimeBillForCustomer(int id, int billId)
        {
            return await _context.timeBills
                .Where(b => b.CustomerId != null && b.Customer != null && b.Customer.Id == id && b.Id == billId)
                .Include(b => b.Employee)
                .Include(b => b.Customer)
                .FirstOrDefaultAsync();
        }
    }
}
