using Microsoft.EntityFrameworkCore;
using Store.DAL.Entities;

namespace Store.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityByDefaultColumns();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ProcessedAgentData> ProcessedAgentDatas { get; set; }
    }
}