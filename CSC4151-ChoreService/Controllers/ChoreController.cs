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
    [Route("chore")]
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

        [HttpGet]        
        public async Task<IEnumerable<Chore>> Get()
        {
            _logger.LogInformation("Getting all Chores");
            return (await _choreRepository.GetAllChores()).ToArray();
        }

        [HttpGet("{id}")]
        public async Task<Chore> GetChore(Guid id)
        {
            _logger.LogInformation($"Get Chore {id}");
            var chore = await _choreRepository.GetChore(id);
            return chore;
        }

        [HttpPost]
        public async Task<Chore> CreateChore([FromBody] Chore chore)
        {
            _logger.LogInformation($"Creating Chore");            
            await _choreRepository.CreateChore(chore);
            return chore;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChore(Guid id)
        {
            _logger.LogInformation($"Deleting Chore {id}");
            await _choreRepository.DeleteChore(id);
            return Ok();
        }
    }
}