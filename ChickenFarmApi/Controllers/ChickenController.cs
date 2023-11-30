using ChickenFarmApi.DataAccess;
using ChickenFarmApi.DataAccess.Entities;
using ChickenFarmApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChickenFarmApi.Controllers
{
    [Route("api/[controller]/chickens")]
    [ApiController]
    public class ChickenController : ControllerBase
    {
        private readonly ChickenFarmContext _dbContext;

        public ChickenController(ChickenFarmContext dbcontext)
        {
            _dbContext = dbcontext;
            _dbContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Add a Chicken
        /// </summary>
        /// <param name="Name">The name of the Chicken</param>        
        /// <returns>The newly created Chicken</returns>
        [HttpPost]

        public async Task<ActionResult<ChickenModel>> Add(string Name, CancellationToken token)
        {
            Chicken chickenNew = new()
            {
                Name = Name
            };

            await _dbContext.Chickens.AddAsync(chickenNew, token);
            await _dbContext.SaveChangesAsync(token);

            return Ok(chickenNew);
        }

        /// <summary>
        /// Look up a Chicken by its Id
        /// </summary>
        /// <param name="chickenId">The Id of the Chicken</param>        
        /// <returns>Id and Name of a Chicken</returns>
        [HttpGet("{chickenId}")]
        public async Task<ActionResult<ChickenModel>> GetChicken(int chickenId, CancellationToken token)
        {
            Chicken? chicken = await _dbContext.Chickens.FindAsync(new object[] { chickenId }, token);

            if (chicken == null)
            {
                return NotFound();
            }

            ChickenModel Chicken = new()
            {
                ChickenId = chicken.ChickenId,
                Name = chicken.Name,
            };

            return Ok(Chicken);
        }
        
        /// <summary>
        /// Get List of all Chickens
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Id and Name of all Chickens</returns>
        [HttpGet("allchickens")]

        public async Task<ActionResult<IEnumerable<ChickenModel>>> GetAllChickens(CancellationToken token = default)
        {
            IQueryable<Chicken> Chickens = _dbContext.Chickens;

            IEnumerable<ChickenModel> chickens = await Chickens
                .Select(chickens => new ChickenModel
                {
                    ChickenId = chickens.ChickenId,
                    Name = chickens.Name,

                })
                .ToListAsync(token);

            return Ok(chickens);
        }
        
        /// <summary>
        /// Update a Chicken's Name
        /// </summary>
        /// <param name="chickenId">The Id of the Chicken you wish to update</param>
        /// <param name="Name">The Updated Name of the Chicken</param>
        /// <returns>Id and updated Name of a Chicken</returns>
        [HttpPatch]
        public async Task<ActionResult<ChickenModel>> ChickenUpdate(int chickenId, string Name)
        {
            Chicken? chickenUpdate = await _dbContext.Chickens.FindAsync(chickenId);

            if (chickenUpdate == null)
            {
                return NotFound();
            }

            chickenUpdate.Name = Name;
            await _dbContext.SaveChangesAsync();

            return new ChickenModel
            {
                ChickenId = chickenUpdate.ChickenId,
                Name = chickenUpdate.Name,
            };
        }
        
        /// <summary>
        /// Delete a Chicken
        /// </summary>
        /// <param name="chickenId">Id of the Chicken to remove</param>
        /// <returns>Chicken successfully deleted</returns>
        [HttpDelete("{chickenId}")]
        public async Task<ActionResult> Delete(int chickenId)
        {
            Chicken? chickenToDelete = await _dbContext.Chickens.FindAsync(chickenId);

            if (chickenToDelete == null)
            {
                return NotFound();
            }

            _dbContext.Chickens.Remove(chickenToDelete);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
