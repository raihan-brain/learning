using learning.Data;
using learning.Data.Entities;
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

        [HttpPost(Name = "SaveTimeBill")]
        [Produces("application/json")]
        public async Task<ActionResult<TimeBill>> SaveTimeBill([FromBody] TimeBill timeBill)
        {
            try
            {
                _repository.AddEntity(timeBill);
                if (await _repository.SaveChanges())
                {
                    return CreatedAtRoute("GetOneTimeBill", new { id = timeBill.Id }, timeBill);
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
