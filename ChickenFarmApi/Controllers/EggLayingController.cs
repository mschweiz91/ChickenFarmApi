using ChickenFarmApi.DataAccess;
using ChickenFarmApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChickenFarmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EggLayingController : ControllerBase
    {
        private readonly EggLayingContext _dbContext;

        public EggLayingController(EggLayingContext context)
        {
            _dbContext = context;
            _dbContext.Database.EnsureCreated();

        }

        [HttpPost("{year}/{month}")]
        public async Task<ActionResult> RecordEggLaying(int year, int month, [FromBody] int eggCount)
        {
            var record = await _dbContext.EggLayingRecords
                .Where(r => r.Year == year && r.Month == month)
                .FirstOrDefaultAsync();

            if (record == null)
            {
                record = new EggLayingRecord { Year = year, Month = month, EggCount = eggCount };
                _dbContext.EggLayingRecords.Add(record);
            }
            else
            {
                record.EggCount = eggCount;
            }

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{year}/{month}")]
        public async Task<ActionResult<int>> GetEggCount(int year, int month)
        {
            var record = await _dbContext.EggLayingRecords
                .Where(r => r.Year == year && r.Month == month)
                .FirstOrDefaultAsync();

            if (record != null)
            {
                return record.EggCount;
            }

            return NotFound();
        }

        
    }
}
