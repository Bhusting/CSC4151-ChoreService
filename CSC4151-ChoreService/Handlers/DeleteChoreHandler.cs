using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CSC4151_ChoreService.Handlers
{
    public class DeleteChoreHandler : IMessageHandler
    {
        private readonly ILogger<DeleteChoreHandler> _logger;
        private readonly IChoreRepository _choreRepository;

        public DeleteChoreHandler(ILogger<DeleteChoreHandler> logger, IChoreRepository choreRepository)
        {
            _logger = logger;
            _choreRepository = choreRepository;
        }

        public async Task Handle(string messageBody)
        {
            var choreId = JsonConvert.DeserializeObject<Guid>(messageBody);

            _logger.LogInformation($"Deleting Chore {choreId}");

            await _choreRepository.DeleteChore(choreId);
        }
    }
}
