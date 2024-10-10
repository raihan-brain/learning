using learning.Data;
using learning.Data.Entities;
using learning.Filters;
using learning.Models;
using learning.Validators;
using Microsoft.AspNetCore.Mvc;

namespace learning.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TimeBillController : Controller
    {
        private readonly IBillingRepository _repository;
        private readonly ILogger<TimeBillController> _logger;

        public TimeBillController(IBillingRepository repository, ILogger<TimeBillController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{id:int}", Name = "GetOneTimeBill")]
        [Produces("application/json")]
        public async Task<ActionResult<TimeBill>> GetOneTimeBill(int id)
        {
            try
            {
                var timeBill = await _repository.GetTimeBill(id);
                if (timeBill == null)
                {
                    return NotFound();
                }
                return Ok(timeBill);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown while reading time bill by ID");
                return Problem($"Exception thrown: {ex.Message}");
            }
        }

        // ... other code ...

        [HttpPost(Name = "SaveTimeBill")]
        [Produces("application/json")]
        [ServiceFilter(typeof(ValidateModelFilter<TimeBillModel>))]// Ensure ValidateEndPointFilter is defined in the learning.Filters namespace
        public async Task<ActionResult<TimeBill>> SaveTimeBill([FromBody] TimeBillModel model)
        {
            try
            {

                var newEntity = new TimeBill()
                {
                    EmployeeId = model.EmployeeId,
                    CustomerId = model.CustomerId,
                    Hours = model.HoursWorked,
                    BillingRate = model.Rate,
                    Date = model.Date,
                    WorkedPerformed = model.Work
                };
                _repository.AddEntity(newEntity);
                if (await _repository.SaveChanges())
                {
                    var newBill = await _repository.GetTimeBill(newEntity.Id);
                    return CreatedAtRoute("GetOneTimeBill", new { id = newEntity.Id }, newBill);
                }
                else
                {
                    return BadRequest("Failed to save time bill");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown while saving time bill");
                return Problem($"Exception thrown: {ex.Message}");
            }
        }
    }
}
