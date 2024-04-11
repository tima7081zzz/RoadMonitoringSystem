using System;
using Agent.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Agent.Services
{
    public class AppConfig : IAppConfig
    {
        private readonly IConfigurationRoot _configurationRoot;

        public AppConfig()
        {
            _configurationRoot = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public string MqttBroker => _configurationRoot["MqttBroker"]!;
        public int MqttPort => GetIntValueFromConfig("MqttPort");
        public string MqttTopic => _configurationRoot["MqttTopic"]!;
        public string MqttUsername => _configurationRoot["MqttUsername"]!;
        public string MqttPassword => _configurationRoot["MqttPassword"]!;

        private int GetIntValueFromConfig(string name)
        {
            return int.Parse(_configurationRoot[name]!);
        }
    }
}