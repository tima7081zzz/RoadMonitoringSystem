namespace Agent.Services.Interfaces
{
    public interface ICustomLogger
    {
        void Information(string message);
        void Error(string message);
        void Warning(string message);
    }
}