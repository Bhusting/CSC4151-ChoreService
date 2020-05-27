using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Settings;
using PusherServer;

namespace CSC4151_ChoreService.Pusher
{
    public class LazyPusher
    {
        private readonly PusherSettings _settings;

        public PusherServer.Pusher Instance { get { return PusherClient.Value; } }

        private Lazy<PusherServer.Pusher> PusherClient => new Lazy<PusherServer.Pusher>(() => new PusherServer.Pusher(_settings.AppId, _settings.Key, _settings.Secret));

        public LazyPusher(PusherSettings settings)
        {
            _settings = settings;
        }

    }
}
