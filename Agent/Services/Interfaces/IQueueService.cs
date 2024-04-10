using System.Threading.Tasks;
using Common;

namespace Agent.Services.Interfaces
{
    public interface IQueueService
    {
        Task Publish<T>(T message);
    }
}