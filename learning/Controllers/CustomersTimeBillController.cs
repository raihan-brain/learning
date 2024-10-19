using learning.Data;
using learning.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace learning.Controllers
{
    [ApiController]
    [Route("/api/customers/{id:int}/timebills")]
    public class CustomersTimeBillController : Controller
    {
        private readonly ILogger<CustomersTimeBillController> _logger;
        private readonly IBillingRepository _repository;

        public CustomersTimeBillController(ILogger<CustomersTimeBillController> logger, IBillingRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet(Name = "GetCustomerTimeBills")]
        public async Task<ActionResult<IEnumerable<TimeBill>>> GetCustomerTimeBills(int id)
        {
            try
            {
                var result = await _repository.GetTimeBillsForCustomer(id);
                if (result == null)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get timebills from database");
                return Problem($"Exception thrown: {ex.Message}");
            }
        }

        [HttpGet("{billId:int}", Name = "GetCustomerTimeBill")]
        public async Task<ActionResult<TimeBill>> GetCustomerTimeBill(int id, int billId)
        {
            try
            {
                var result = await _repository.GetTimeBillForCustomer(id, billId);
                if (result == null)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get timebill from database");
                return Problem($"Exception thrown: {ex.Message}");
            }
        }
    }
}
