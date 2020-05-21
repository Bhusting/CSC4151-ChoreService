using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Builders;
using Common.Clients;
using Common.Repositories;
using Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CSC4151_ChoreService.Handlers
{
    public class CreateChoreHandler : IMessageHandler
    {
        private readonly ILogger<CreateChoreHandler> _logger;
        private readonly IChoreRepository _choreRepository;

        public CreateChoreHandler(ILogger<CreateChoreHandler> logger, IChoreRepository choreRepository)
        {
            _logger = logger;
            _choreRepository = choreRepository;
        }

        public async Task Handle(string messageBody)
        {
            var chore = JsonConvert.DeserializeObject<Chore>(messageBody);

            _logger.LogInformation($"Creating Chore {chore.ChoreId}");

            await _choreRepository.CreateChore(chore);
        }
    }
}
