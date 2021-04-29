using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Dapper.WebApi.Models.Config;
using Microsoft.Extensions.Hosting;

namespace AspNetCore.Dapper.WebApi.Services
{
    public class SyncDataWorker : IHostedService
    {
     private const int SYNC_TIME_SECS = 30; // minute
        private readonly AppSettings appSettings;
        private readonly IServiceProvider serviceProvider = null;
        private readonly DataService dataService;
        private Timer timer = null;
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = new Timer(this.DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(SYNC_TIME_SECS));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer?.Change(Timeout.Infinite, 0);
            await Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var totalCnt = this.dataService.SyncAsync().Result;
            Debug.WriteLine($"{totalCnt} records had been synchorized.");
        }
    }
}
