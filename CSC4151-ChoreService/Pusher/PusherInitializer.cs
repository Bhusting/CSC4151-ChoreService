using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace CSC4151_ChoreService.Pusher
{
    public class PusherInitializer : BackgroundService
    {
        private readonly PusherClient _pusher;

        public PusherInitializer(PusherClient pusher)
        {
            _pusher = pusher;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _pusher.StartAsync();

            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {

            await base.StopAsync(cancellationToken);

        }
    }
}
