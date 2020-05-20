using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using Domain;
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

        [HttpGet("{id}")]
        public async Task<Chore> GetChore(Guid id)
        {
            _logger.LogInformation($"Get Chore {id}");
            var chore = await _choreRepository.GetChore(id);
            return chore;
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