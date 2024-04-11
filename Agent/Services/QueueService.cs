using System;
using System.Text.Json;
using System.Threading.Tasks;
using Agent.Services.Interfaces;
using MQTTnet;
using MQTTnet.Client;

namespace Agent.Services
{
    public class QueueService : IQueueService
    {
        private readonly ICustomLogger _logger;
        private readonly IAppConfig _appConfig;

        //private IMqttClient _mqttClient;

        public QueueService(ICustomLogger logger, IAppConfig appConfig)
        {
            _logger = logger;
            _appConfig = appConfig;
        }

        public async Task Publish<T>(T message)
        {
            var mqttMessage = new MqttApplicationMessageBuilder()
                .WithTopic(_appConfig.MqttTopic)
                .WithPayload(JsonSerializer.SerializeToUtf8Bytes(message))
                .Build();

            using var client = await Connect();
            await client.PublishAsync(mqttMessage);
            await client.DisconnectAsync();
        }

        public async Task Run()
        {

        }

        private async Task<IMqttClient> Connect()
        {
            var client = new MqttFactory().CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("mqtt", _appConfig.MqttPort)
                .WithCredentials(_appConfig.MqttUsername, _appConfig.MqttPassword)
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