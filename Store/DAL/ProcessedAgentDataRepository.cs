using System.Collections.Generic;
using System.Linq;
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

        public async Task Delete(int id)
        {
            await _entities
                .Where(x=> x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<ProcessedAgentData> Add(ProcessedAgentData data) => (await _entities.AddAsync(data)).Entity;

        public async Task Add(IEnumerable<ProcessedAgentData> data) => await _entities.AddRangeAsync(data);

        public void Update(ProcessedAgentData data) => _entities.Update(data);
    }
}