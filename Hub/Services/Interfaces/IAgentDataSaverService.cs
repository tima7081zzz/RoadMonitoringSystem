using System.Threading.Tasks;
using Hub.Models;

namespace Hub.Services.Interfaces
{
    public interface IAgentDataSaverService
    {
        Task Save(ProcessedAgentData data);
    }
}