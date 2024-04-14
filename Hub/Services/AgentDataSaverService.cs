using System;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Hub.Models;
using Hub.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;

namespace Hub.Services
{
    public class AgentDataSaverService : IAgentDataSaverService, IHostedService
    {
        private readonly ILogger _logger;
        private readonly IOptionsSnapshot<HubOptions> _options;
        private IStoreClient _storeClient;
        private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

        private readonly ConcurrentBag<ProcessedAgentData> _datas = new();

        public AgentDataSaverService(IServiceScopeFactory scopeFactory, IOptionsSnapshot<HubOptions> options, ILoggerFactory loggerFactory)
        {
            _options = options;
            _logger = loggerFactory.CreateLogger<AgentDataSaverService>();
            _storeClient = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IStoreClient>();
        }

        private async Task ListenToTopic()
        {
            var client = await Connect();
            await client.SubscribeAsync(_options.Value.MqttTopic);

            client.ApplicationMessageReceivedAsync += async e =>
            {
                var message = JsonSerializer.Deserialize<ProcessedAgentData>(e.ApplicationMessage.PayloadSegment.Array);
                if (message is null)
                {
                    _logger.LogError("Failed to deserialize message payload");
                    return;
                }

                await Save(message);
            };
        }

        public async Task Save(ProcessedAgentData data)
        {
            try
            {
                await _semaphoreSlim.WaitAsync();

                _datas.Add(data);
                if (_datas.Count < 1)
                {
                    return;
                }

                await _storeClient.BulkAdd(_datas);
                _logger.LogInformation($"{_datas.Count} messages were saved");

                _datas.Clear();
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        private async Task<IMqttClient> Connect()
        {
            var client = new MqttFactory().CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("mqtt", _options.Value.MqttPort)
                .WithCredentials(_options.Value.MqttUsername, _options.Value.MqttPassword)
                .WithClientId(Guid.NewGuid().ToString())
                .WithCleanStart()
                .Build();

            var connectResult = await client.ConnectAsync(options);
            if (connectResult.ResultCode is not MqttClientConnectResultCode.Success)
            {
                throw new Exception("Failed to connect MQTT server");
            }

            return client;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //await ListenToTopic();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}