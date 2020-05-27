using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Repositories;
using CSC4151_ChoreService.Pusher;
using Domain;
using Newtonsoft.Json;
using PusherServer;

namespace CSC4151_ChoreService.Handlers
{
    public class CompleteChoreHandler : IMessageHandler
    {
        private readonly IChoreRepository _choreRepository;
        private readonly LazyPusher _pusher;

        public CompleteChoreHandler(IChoreRepository choreRepository, LazyPusher pusher)
        {
            _choreRepository = choreRepository;
            _pusher = pusher;
        }

        public async Task Handle(string messageBody)
        {
            var choreId = JsonConvert.DeserializeObject<Guid>(messageBody);

            var chore = await _choreRepository.GetChore(choreId);

            await _choreRepository.UpdateChore(chore, true);

            var houseClient = new HttpClient() {BaseAddress = new Uri("https://takprofile.azurewebsites.net/") };

            var res = await houseClient.GetAsync($"House/{chore.HouseId.ToString()}");

            if (res.IsSuccessStatusCode)
            {
                var house = JsonConvert.DeserializeObject<House>(await res.Content.ReadAsStringAsync());

                var ev = new Event[1];
                ev[0].Channel = house.Channel.ToString();
                ev[0].EventName = "ChoreComplete";
                ev[0].Data = new {message = chore.ChoreId.ToString()};


                await _pusher.Instance.TriggerAsync(ev);
            }
        }
    }
}
