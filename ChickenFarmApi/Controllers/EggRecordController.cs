using ChickenFarmApi.DataAccess;
using ChickenFarmApi.DataAccess.Entities;
using ChickenFarmApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

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
        [HttpPost("{chickenid}")]
        public async Task<ActionResult<EggLayingRecordModel>> AddEggRecord(EggLayingRecordModel record, CancellationToken token)
        {
            EggLayingRecord recordNew = new()
            {
                ChickenId = record.ChickenId,
                RecordId = record.RecordId,
                Name = record.Name,
                Year = record.Year,
                Month = record.Month,
                EggCount = record.EggCount
            };

            await _dbContext.EggLayingRecords.AddAsync(recordNew, token);
            await _dbContext.SaveChangesAsync(token);

            record.RecordId = recordNew.RecordId;

            return Ok(record);



            //var record = await _dbContext.EggLayingRecords
            //    .Where(r => r.Year == year && r.Month == month && r.ChickenId == chickenId && r.RecordId == recordId)
            //    .FirstOrDefaultAsync();

            //if (recordNew == null)
            //{
            //    record = new EggLayingRecord { ChickenId = chickenId, RecordId = recordId, Year = year, Month = month, EggCount = eggCount, };
            //    _dbContext.EggLayingRecords.Add(record);
            //}


            //await _dbContext.SaveChangesAsync(token);

            //return Ok();
        }

        //Read method to retrieve the egg laying records for a specific chicken
        [HttpGet("{chickenid}")]
        public async Task<ActionResult<EggLayingRecordModel>> GetEggCount(int chickenId, CancellationToken token)
        {
            EggLayingRecord? eggRecord = await _dbContext.EggLayingRecords.FindAsync(new object[] { chickenId }, token);
            //.Where(r => r.Year == year && r.Month == month && r.ChickenId == chickenId)
            //.FirstOrDefaultAsync();

            if (eggRecord == null)
            {
                return NotFound();
            }

            EggLayingRecordModel eggLayingRecord = new()
            {
                ChickenId = eggRecord.ChickenId,
                RecordId = eggRecord.RecordId,
                Name = eggRecord.Name,
                Year = eggRecord.Year,
                Month = eggRecord.Month,
                EggCount = eggRecord.EggCount

            };

            return Ok(eggLayingRecord);
        }

        //Read method to get a record of total eggs layed in a given year & month by all chickens
        [HttpGet("eggtotal")]
        public async Task<ActionResult<int>> GetSumOfEggs(int year, int month)
        {
            var sum = await _dbContext.EggLayingRecords
                .Where(r => r.Year == year && r.Month == month)
                .SumAsync(r => r.EggCount);

            return Ok(sum);
        }

        //Read method to get a list of all egg laying records
        [HttpGet("allrecords")]
        public async Task<ActionResult<IEnumerable<EggLayingRecordModel>>> GetAllEggRecords(CancellationToken token = default)
        {
            IQueryable<EggLayingRecord> eggLayingRecords = _dbContext.EggLayingRecords;

            IEnumerable<EggLayingRecordModel> eggRecords = await eggLayingRecords
                .Select(eggRecords => new EggLayingRecordModel
                {
                    ChickenId = eggRecords.ChickenId,
                    RecordId = eggRecords.RecordId,
                    Name = eggRecords.Name,
                    Year = eggRecords.Year,
                    Month = eggRecords.Month,
                    EggCount = eggRecords.EggCount
                })
                .ToListAsync(token);

            return Ok(eggRecords);
        }

        //Update method to update an egg laying record of a chicken
        [HttpPatch("{recordid}")]
        public async Task<ActionResult<EggLayingRecordModel>> Update(EggLayingRecordModel record)
        {
            EggLayingRecord? recordToUpdate = await _dbContext.EggLayingRecords.FindAsync(record.RecordId);

            if (recordToUpdate == null)
            {
                return NotFound();
            }

            recordToUpdate.Year = record.Year;
            recordToUpdate.Month = record.Month;
            recordToUpdate.EggCount = record.EggCount;
            await _dbContext.SaveChangesAsync();

            return new EggLayingRecordModel
            {
                Year = recordToUpdate.Year,
                Month = recordToUpdate.Month,
                EggCount = recordToUpdate.EggCount
            };
        }

        //Delete method to delete an egg laying record of a chicken
        [HttpDelete("{recordid}")]
        public async Task<ActionResult> Delete(Guid recordId)
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
