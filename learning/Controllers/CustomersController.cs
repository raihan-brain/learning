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
        public async Task<ActionResult<IEnumerable<Customer>>> Get(bool withAddress = false)
        {
            try
            {
                IEnumerable<Customer> result;
                if (withAddress)
                {
                    result = await _repository.GetCustomersWithAddress();
                }
                else
                {
                    result = await _repository.GetCustomers();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get customers from database");
                return Problem($"Exception thrown: {ex.Message}");
            }
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
                return Problem($"Exception thrown: {ex.Message}");
            }
        }
        [HttpPost(Name = "SaveCustomer")]
        [Produces("application/json")]
        public async Task<ActionResult<Customer>> SaveCustomer(Customer customer)
        {
            try
            {
                _repository.AddEntity(customer);
                if (await _repository.SaveChanges())
                {
                    return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
                }
                else
                {
                    return BadRequest("Failed to save customer");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown while saving customer data");
                return Problem($"Exception thrown: {ex.Message}");
            }
        }

        // write a method that will save customer data to db

    }
}
