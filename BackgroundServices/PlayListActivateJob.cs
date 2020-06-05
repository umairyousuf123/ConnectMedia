using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using System.Threading;
using ConnectMedia.BackgroundServices.Services.Interface;

namespace ConnectMedia.BackgroundServices
{
    public class PlayListActivateJob : IJob
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<PlayListActivateJob> logger;
        // private readonly IBackgroundExecutionService backgroundExecutionService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public PlayListActivateJob(IConfiguration configuration, ILogger<PlayListActivateJob> logger,
            IServiceScopeFactory serviceScopeFactory// IBackgroundExecutionService _backgroundExecutionService
            )
        {
            this.logger = logger;
            this.configuration = configuration;
            _serviceScopeFactory = serviceScopeFactory;
            // this.backgroundExecutionService = _backgroundExecutionService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                      var BackgroundExecutionService = scope.ServiceProvider.GetService<IBackgroundExecutionService>();
                      await  BackgroundExecutionService.ActivatePlayList();
                 

                }

            }
            catch (Exception ex)
            {
            }
            await Task.CompletedTask;
        }
    }
}
