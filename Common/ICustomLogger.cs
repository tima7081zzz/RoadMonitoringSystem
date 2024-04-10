namespace Common
{
    public interface ICustomLogger
    {
        void Information(string message);
        void Error(string message);
        void Warning(string message);
    }
}