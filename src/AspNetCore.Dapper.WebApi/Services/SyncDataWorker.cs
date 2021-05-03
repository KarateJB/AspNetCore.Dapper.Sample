using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Dapper.WebApi.Models.Config;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNetCore.Dapper.WebApi.Services
{
    public class SyncDataWorker : IHostedService
    {
     private const int SYNC_TIME_SECS = 30; // minute
        private readonly ILogger<SyncDataWorker> logger;
        private readonly AppSettings appSettings;
        private readonly IServiceProvider serviceProvider = null;
        private readonly FolkDalService folkDalService;
        private Timer timer = null;

        public SyncDataWorker(ILogger<SyncDataWorker> logger, IOptions<AppSettings> configuration, FolkDalService folkDalService)
        {
            this.logger = logger;
            this.appSettings = configuration.Value;
            this.folkDalService = folkDalService;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = new Timer(this.DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(SYNC_TIME_SECS));
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer?.Change(Timeout.Infinite, 0);
            await Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var totalCnt = this.folkDalService.SyncAsync(this.appSettings?.SyncDates?.StartDate, this.appSettings?.SyncDates?.EndDate).Result;
            Debug.WriteLine($"{totalCnt} records had been synchorized.");
        }
    }
}
