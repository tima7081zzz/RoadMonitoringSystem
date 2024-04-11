namespace Common
{
    public interface IAppConfig
    {
        string MqttBroker { get; }
        int MqttPort { get; }
        string MqttTopic { get; }
        string MqttUsername { get; }
        string MqttPassword { get; }
    };
}