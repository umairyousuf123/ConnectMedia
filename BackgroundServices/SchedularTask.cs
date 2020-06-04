


using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System.Threading;
using System.Threading.Tasks;


namespace ConnectMedia.BackgroundServices
{
    public class SchedularTask : BackgroundService, IHostedService
    {
        private readonly ILogger _logger;
        private Timer _timer;
        public IServiceScopeFactory _serviceScopeFactory;
        public Task _InstallationTask;
        public Task _PaymentTask;

        public SchedularTask(ILogger<SchedularTask> logger, IServiceScopeFactory serviceScopeFactory)
        {

            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _InstallationTask.Start();
        }
    }
}
