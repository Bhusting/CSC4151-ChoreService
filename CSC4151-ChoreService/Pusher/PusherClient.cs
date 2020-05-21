using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Settings;
using PusherServer;

namespace CSC4151_ChoreService.Pusher
{
    public class PusherClient
    {
        private PusherServer.Pusher _pusher;
        private readonly PusherSettings _settings;

        public PusherClient(PusherSettings settings)
        {
            _settings = settings;
        }

        public async Task StartAsync()
        {
            _pusher = new PusherServer.Pusher(_settings.AppId, _settings.Key, _settings.Secret, new PusherOptions() { Cluster = "us3" });
        }

        public async Task<ITriggerResult> SendEvent(Guid channel, string eventName, object data)
        {
            var res = await _pusher.TriggerAsync(channel.ToString(), eventName, data);

            return res;
        }
    }
}
