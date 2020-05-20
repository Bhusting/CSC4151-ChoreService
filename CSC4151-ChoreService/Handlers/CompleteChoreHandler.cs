using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSC4151_ChoreService.Pusher;

namespace CSC4151_ChoreService.Handlers
{
    public class CompleteChoreHandler : IMessageHandler
    {
        private readonly PusherClient _pusher;

        public CompleteChoreHandler(PusherClient pusher)
        {
            _pusher = pusher;
        }

        public async Task Handle(string messageBody)
        {



        }
    }
}
