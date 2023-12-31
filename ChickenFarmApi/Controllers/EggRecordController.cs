﻿using ChickenFarmApi.DataAccess;
using ChickenFarmApi.DataAccess.Entities;
using ChickenFarmApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        
        /// <summary>
        /// Create new Egg Laying Record
        /// </summary>
        /// <param name="ChickenId">Id of the Chicken</param>
        /// <param name="Year">Year of the new Record</param>
        /// <param name="Month">Month of the new Record</param>
        /// <param name="EggCount">Egg Count of the new Record</param>        
        /// <returns>Newly created Egg Laying Record</returns>
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
        
        /// <summary>
        /// Get Egg Laying Records for a Chicken
        /// </summary>
        /// <param name="chickenId">Id of the Chicken</param>        
        /// <returns>All Egg Laying Records of the specified Chicken</returns>
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
        
        /// <summary>
        /// Get Sum Total for Eggs in a given Year and Month
        /// </summary>
        /// <param name="year">Year of the Record</param>
        /// <param name="month">Month of the Record</param>
        /// <returns>Sum Total for Eggs laid by all Chickens in a given Year and Month</returns>
        [HttpGet("eggtotal")]
        public async Task<ActionResult<int>> GetSumOfEggs(int year, int month)
        {
            var sum = await _dbContext.EggLayingRecords
                .Where(r => r.Year == year && r.Month == month)
                .SumAsync(r => r.EggCount);

            return Ok($"Total eggs laid: " + sum);
        }
        
        /// <summary>
        /// Get List of All Egg Laying Records
        /// </summary>        
        /// <returns>List of All Egg Laying Records</returns>
        [HttpGet("allrecords")]
        public async Task<ActionResult<IEnumerable<EggLayingRecordModel>>> GetAllEggRecords(CancellationToken token = default)
        {

            var eggLayingRecords = await _dbContext.EggLayingRecords
                .Include(r => r.Chicken) //Includes the related Chicken entity
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
        
        /// <summary>
        /// Update an Egg Laying Record
        /// </summary>
        /// <param name="recordId">RecordId</param>
        /// <param name="EggCount">Egg Count to Update</param>
        /// <param name="Year">Year to Update</param>
        /// <param name="Month">Month to Update</param>
        /// <returns>Updated Egg Laying Record</returns>
        [HttpPatch]
        public async Task<ActionResult<EggLayingRecordModel>> UpdateEggRecord(int recordId, int EggCount, int? Year = null, int? Month = null)
        {

            EggLayingRecord? updatedRecord = await _dbContext.EggLayingRecords
                .Include(r => r.Chicken) //Includes the related Chicken entity
                .FirstOrDefaultAsync(r => r.RecordId == recordId);

            if (updatedRecord == null)
            {
                return NotFound($"Egg laying record with Id {recordId} could not be found");
            }

            if (Year != null)
            {
                updatedRecord.Year = Year.Value;
            }

            if (Month != null)
            {
                updatedRecord.Month = Month.Value;
            }

            updatedRecord.EggCount = EggCount;
            await _dbContext.SaveChangesAsync();

            return new EggLayingRecordModel
            {
                ChickenId = updatedRecord.ChickenId,
                RecordId = updatedRecord.RecordId,
                Name = updatedRecord.Chicken.Name,
                Year = updatedRecord.Year,
                Month = updatedRecord.Month,
                EggCount = updatedRecord.EggCount
            };
        }
        
        /// <summary>
        /// Delete an Egg Laying Record
        /// </summary>
        /// <param name="recordId">RecordId</param>
        /// <returns>Egg Laying Record successfully deleted</returns>
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
