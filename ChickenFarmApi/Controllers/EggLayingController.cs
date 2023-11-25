using ChickenFarmApi.DataAccess;
using ChickenFarmApi.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChickenFarmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EggLayingController : ControllerBase
    {
        private readonly EggLayingContext _context;

        public EggLayingController(EggLayingContext context)
        {
            _context = context;
        }

        [HttpPost("{year}/{month}")]
        public async Task<ActionResult> RecordEggLaying(int year, int month, [FromBody] int eggCount)
        {
            var record = await _context.EggLayingRecords
                .Where(r => r.Year == year && r.Month == month)
                .FirstOrDefaultAsync();

            if (record == null)
            {
                record = new EggLayingRecord { Year = year, Month = month, EggCount = eggCount };
                _context.EggLayingRecords.Add(record);
            }
            else
            {
                record.EggCount = eggCount;
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{year}/{month}")]
        public async Task<ActionResult<int>> GetEggCount(int year, int month)
        {
            var record = await _context.EggLayingRecords
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
