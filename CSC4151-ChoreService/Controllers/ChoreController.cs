using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using Domain;
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
        

        [HttpGet("House/{houseId}")]
        public async Task<IEnumerable<Chore>> GetByHouseId(Guid houseId)
        {
            _logger.LogInformation("Getting all Chores by HouseId");

            var chores = await _choreRepository.GetAllChoresByHouseId(houseId);

            return chores;
        }

        [HttpGet("House/{houseId}/Today")]
        public async Task<IEnumerable<Chore>> GetByHouseIdToday(Guid houseId)
        {
            _logger.LogInformation("Getting all Chores by HouseId for Today");

            var chores = await _choreRepository.GetAllChoresByHouseIdToday(houseId);
            
            return chores;
        }

        [HttpGet("ChoreType/{choreTypeId}")]
        public async Task<IEnumerable<Chore>> GetByChoreTypeId(short choreTypeId)
        {
            _logger.LogInformation("Getting all Chores by ChoreTypeId");
            return await _choreRepository.GetAllChoresByChoreTypeId(choreTypeId);
        }

        [HttpGet("{choreId}")]
        public async Task<Chore> GetChore(Guid choreId)
        {
            _logger.LogInformation($"Get Chore {choreId}");
            var chore = await _choreRepository.GetChore(choreId);
            return chore;
        }
    }
}
