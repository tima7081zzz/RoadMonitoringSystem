using System.Collections.Generic;
using System.Threading.Tasks;
using Store.DAL.Entities;
using Store.Models;

namespace Store.Services.Interfaces
{
    public interface IProcessedAgentDataService
    {
        Task<ProcessedAgentData> Get(int id);
        Task Delete(int id);
        Task<ProcessedAgentData> Add(ProcessedAgentDataRequestModel data);
        Task BulkAdd(IEnumerable<ProcessedAgentDataRequestModel> data);
        Task Update(int id, ProcessedAgentDataRequestModel data);
    }
}