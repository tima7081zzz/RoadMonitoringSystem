using System.Threading;
using System.Threading.Tasks;
using Agent.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Agent.Services
{
    public class SensorService : ISensorService, IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public SensorService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task PublishSensorsData()
        {
            await using var scope = _scopeFactory.CreateAsyncScope();

            using var csvDataReader = scope.ServiceProvider.GetRequiredService<ICsvDataReader>();
            var queueService = scope.ServiceProvider.GetRequiredService<IQueueService>();
            var options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<AgentOptions>>();

            while (true)
            {
                await Task.Delay(options.Value.PublishDelay);

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