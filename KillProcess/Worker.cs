using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace KillProcess
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            this._serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var opts = _serviceProvider.GetService<IOptionsSnapshot<KillProcessOptions>>().Value;
                foreach (var item in opts.ProcessStartTimeDic)
                {
                    Kill(item.Key,DateTime.Now.AddMilliseconds(-1*item.Value));
                }
                await Task.Delay(opts.ExecutionInterval, stoppingToken);
            }
        }

        private void Kill(string processName,DateTime beforeTime)
        {
            var processes = Process.GetProcessesByName(processName).Where(e => e.StartTime <= beforeTime).ToArray();
            if (processes.Any())
            {
                _logger.LogInformation($"发现{processes.Length}个名为{processName}的进程,开始杀进程");
            }
            foreach (var process in processes)
            {
                try
                {
                    process.Kill();
                }
                catch(Exception e)
                {
                    _logger.LogWarning(e,$"杀{beforeTime:yyyy-MM-dd HH:mm:ss:sss}之前的{processName}(pid:{process.Id})进程失败");
                }
            }
        }
    }
}
