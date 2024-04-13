using System;
using System.Threading;
using System.Threading.Tasks;
using Agent.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Agent.Services
{
    public class SensorService : ISensorService, IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly TimeSpan _publishDelay = TimeSpan.FromMilliseconds(50);

        public SensorService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task PublishSensorsData()
        {
            await using var scope = _scopeFactory.CreateAsyncScope();

            using var csvDataReader = scope.ServiceProvider.GetRequiredService<ICsvDataReader>();
            var queueService = scope.ServiceProvider.GetRequiredService<IQueueService>();

            while (true)
            {
                await Task.Delay(_publishDelay);

                var data = await csvDataReader.Read();
                if (data is null)
                {
                    break;
                }

                await queueService.Publish(data);
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await PublishSensorsData();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}