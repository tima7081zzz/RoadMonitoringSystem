using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.DAL.Entities;

namespace Store.DAL
{
    public class ProcessedAgentDataRepository
    {
        private readonly DbSet<ProcessedAgentData> _entities;

        public ProcessedAgentDataRepository(DbSet<ProcessedAgentData> entities) => _entities = entities;

        public async Task<ProcessedAgentData?> Get(int id)
        {
            return await _entities.FindAsync(id);
        }
    }
}