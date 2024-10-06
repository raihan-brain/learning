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
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(IBillingRepository repository, ILogger<CustomersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet(Name = "GetCustomers")]
        [Produces("application/json")]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _repository.GetCustomers();
        }

        [HttpGet("{id:int}", Name = "GetCustomer")]
        [Produces("application/json")]
        public async Task<ActionResult<Customer>> GetOne(int id)
        {
            try
            {
                var customer = await _repository.GetCustomer(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown while reading cudtomer by ID   ");
                return Problem($"Exception thrown: {ex.Message }");
            }
        }
    }
}
