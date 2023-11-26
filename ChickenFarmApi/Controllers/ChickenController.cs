using ChickenFarmApi.DataAccess;
using ChickenFarmApi.Models;
using ChickenFarmApi.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System;
using ChickenFarmApi.DataAccess.Migrations;

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

        //Create method to add a chicken
        [HttpPost]

        public async Task<ActionResult<ChickenModel>> Add(ChickenModel chicken, CancellationToken token)
        {
            Chicken chickenNew = new()
            {
                ChickenId = chicken.ChickenId,
                Name = chicken.Name
            };

            await _dbContext.Chickens.AddAsync(chickenNew, token);
            await _dbContext.SaveChangesAsync(token);

            chicken.ChickenId = chickenNew.ChickenId;

            return Ok(chicken);
        }


        //Read method to retrieve data on a specific chicken
        [HttpGet("{chickenid}")]
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
                Name = chicken.Name
            };

            return Ok(Chicken);
        }

        //Read method to get a list of all chickens
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
                

        //Update method to update a specific chicken's data
        [HttpPatch("{chickenid}")]
        public async Task<ActionResult<ChickenModel>> ChickenUpdate(ChickenModel chicken)
        {
            Chicken? chickenUpdate = await _dbContext.Chickens.FindAsync(chicken.ChickenId);

            if (chickenUpdate == null)
            {
                return NotFound();
            }

            chickenUpdate.ChickenId = chicken.ChickenId;
            chickenUpdate.Name = chicken.Name;
            await _dbContext.SaveChangesAsync();

            return new ChickenModel
            {
                ChickenId = chickenUpdate.ChickenId,
                Name = chickenUpdate.Name,
            };
                       

        }

        //Delete method to delete a chicken
        [HttpDelete("{chickenid}")]
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
