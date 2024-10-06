using learning.Data;
using learning.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace learning.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IBillingRepository _repository;

        public CustomersController(IBillingRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(Name = "GetCustomers")]
        [Produces("application/json")]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _repository.GetCustomers();
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        [Produces("application/json")]
        public async Task<ActionResult<Customer>> GetOne(int id)
        {
            var customer = await _repository.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }
    }
}
