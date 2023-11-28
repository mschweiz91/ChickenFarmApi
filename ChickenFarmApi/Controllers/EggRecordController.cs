using ChickenFarmApi.DataAccess;
using ChickenFarmApi.DataAccess.Entities;
using ChickenFarmApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml.Linq;

namespace ChickenFarmApi.Controllers
{
    [Route("api/[controller]/egglayingrecords")]
    [ApiController]
    public class EggRecordController : ControllerBase
    {
        private readonly ChickenFarmContext _dbContext;

        public EggRecordController(ChickenFarmContext dbcontext)
        {
            _dbContext = dbcontext;
            _dbContext.Database.EnsureCreated();

        }

        //Create method to add an egg laying record to a specific chicken in a specific month & year
        [HttpPost]
        public async Task<ActionResult<EggLayingRecordModel>> AddEggRecord(int ChickenId, int Year, int Month, int EggCount, CancellationToken token)
        {
            EggLayingRecord recordNew = new()
            {
                ChickenId = ChickenId,            
                Year = Year,
                Month = Month,
                EggCount = EggCount
            };

            await _dbContext.EggLayingRecords.AddAsync(recordNew, token);
            await _dbContext.SaveChangesAsync(token);

            //RecordId = recordNew.RecordId;

            return Ok(recordNew);



          
        }

        //Read method to retrieve the egg laying records for a specific chicken
        [HttpGet("{chickenId}")]
        public async Task<ActionResult<IEnumerable<EggLayingRecordModel>>> GetEggRecords(int chickenId, CancellationToken token)
        {
                        
            var eggLayingRecords = await _dbContext.EggLayingRecords
                .Where(r => r.ChickenId == chickenId)
                .Include(r => r.Chicken)
                .Select(r => new                    
                            
            {
                r.ChickenId,
                r.RecordId,
                ChickenName = r.Chicken.Name,
                r.Year,
                r.Month,
                r.EggCount

            })
                .ToListAsync(token);

            if (eggLayingRecords == null)
            {
                return NotFound();
            }

            return Ok(eggLayingRecords);
        }

        //Read method to get a record of total eggs layed in a given year & month by all chickens
        [HttpGet("eggtotal")]
        public async Task<ActionResult<int>> GetSumOfEggs(int year, int month)
        {
            var sum = await _dbContext.EggLayingRecords
                .Where(r => r.Year == year && r.Month == month)
                .SumAsync(r => r.EggCount);

            return Ok($"Total eggs laid: " + sum);
        }

        //Read method to get a list of all egg laying records
        [HttpGet("allrecords")]
        public async Task<ActionResult<IEnumerable<EggLayingRecordModel>>> GetAllEggRecords(CancellationToken token = default)
        {
            
            var eggLayingRecords = await _dbContext.EggLayingRecords
                .Include(r => r.Chicken) //Inlcudes the related Chicken entity
                .Select(r => new
                      {
                          r.ChickenId,
                          r.RecordId,
                          ChickenName = r.Chicken.Name, //Includes the Name property of the related Chicken
                          r.Year,
                          r.Month,
                          r.EggCount
                      })

                .ToListAsync(token);

            if (eggLayingRecords == null)
            {
                return NotFound();
            }

            return Ok(eggLayingRecords);
        }

        //Update method to update an egg laying record of a chicken
        [HttpPatch]
        public async Task<ActionResult<EggLayingRecordModel>> UpdateEggRecord(int recordId, int Year, int Month, int EggCount)
        {
           
            EggLayingRecord? updatedRecord = await _dbContext.EggLayingRecords
                .Include(r => r.Chicken) //Includes the related Chicken entity
                .FirstOrDefaultAsync(r => r.RecordId == recordId);

            if (updatedRecord == null)
            {
                return NotFound($"Egg laying record with Id {recordId} could not be found");
            }

            updatedRecord.Year = Year;
            updatedRecord.Month = Month;
            updatedRecord.EggCount = EggCount;
            await _dbContext.SaveChangesAsync();

            return new EggLayingRecordModel
            {
                ChickenId = updatedRecord.ChickenId,
                RecordId =updatedRecord.RecordId,
                Name = updatedRecord.Chicken.Name,
                Year = updatedRecord.Year,
                Month = updatedRecord.Month,
                EggCount = updatedRecord.EggCount
        };

            
            
        }

        //Delete method to delete an egg laying record of a chicken
        [HttpDelete("{recordId}")]
        public async Task<ActionResult> Delete(int recordId)
        {
            EggLayingRecord? recordToDelete = await _dbContext.EggLayingRecords.FindAsync(recordId);

            if (recordToDelete == null)
            {
                return NotFound();
            }

            _dbContext.EggLayingRecords.Remove(recordToDelete);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
