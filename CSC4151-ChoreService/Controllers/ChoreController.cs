using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CSC4151_ChoreService.Controllers
{
    [Route("Chore")]
    [ApiController]
    public class ChoreController : ControllerBase
    {
        private readonly ILogger<ChoreController> _logger;
        private readonly IChoreRepository _choreRepository;

        public ChoreController(ILogger<ChoreController> logger, IChoreRepository choreRepository)
        {
            _logger = logger;
            _choreRepository = choreRepository;
        }

        // Getting all records

        //[HttpGet]
        //public async Task<IEnumerable<Chore>> Get()
        //{
        //    _logger.LogInformation("Getting all Chores");
        //    return (await _choreRepository.GetAllChores()).ToArray();
        //}


        [HttpGet("House/{houseId}")]
        public async Task<IEnumerable<Chore>> GetByHouseId(string houseId)
        {
            _logger.LogInformation("Getting all Chores by HouseId");
            return (await _choreRepository.GetAllChoresByHouseId(houseId));
        }

        [HttpGet("ChoreType/{choreTypeId}")]
        public async Task<IEnumerable<Chore>> GetByChoreTypeId(short choreTypeId)
        {
            _logger.LogInformation("Getting all Chores by ChoreTypeId");
            return (await _choreRepository.GetAllChoresByChoreTypeId(choreTypeId));
        }

        [HttpGet("{choreId}")]
        public async Task<Chore> GetChore(Guid choreId)
        {
            _logger.LogInformation($"Get Chore {choreId}");
            var chore = await _choreRepository.GetChore(choreId);
            return chore;
        }

        [HttpPut("{choreId}")]
        public async Task<Chore> UpdateChore(Guid choreId, [FromBody] UpdateChoreModel model)
        {
            _logger.LogInformation($"Update Chore {choreId}");
            
             var result = await _choreRepository.UpdateChore(choreId, model.IsCompleted);

            return result;
        }

        

        /*[HttpGet("{id}")]
        public async Task<IActionResult> GetChoreByChoreId(Guid id)
        {
            _logger.LogInformation($"Creating Chore");
            await _choreRepository.GetChoreByChoreId(id);
            return Ok("Created");
        }*/

    }
}
