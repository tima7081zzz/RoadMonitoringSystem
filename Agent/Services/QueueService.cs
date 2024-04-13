using System;
using System.Text.Json;
using System.Threading.Tasks;
using Agent.Services.Interfaces;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;

namespace Agent.Services
{
    public class QueueService : IQueueService
    {
        private readonly ICustomLogger _logger;
        private readonly IOptionsSnapshot<AgentOptions> _options;

        //private IMqttClient _mqttClient;

        public QueueService(ICustomLogger logger, IOptionsSnapshot<AgentOptions> options)
        {
            _logger = logger;
            _options = options;
        }

        public async Task Publish<T>(T message)
        {
            var mqttMessage = new MqttApplicationMessageBuilder()
                .WithTopic(_options.Value.MqttTopic)
                .WithPayload(JsonSerializer.SerializeToUtf8Bytes(message))
                .Build();

            using var client = await Connect();
            await client.PublishAsync(mqttMessage);
            await client.DisconnectAsync();
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
    }
}