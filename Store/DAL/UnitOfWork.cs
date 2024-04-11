namespace Store.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DataContext dataContext)
        {
            dataContext.Database.EnsureCreated();

            ProcessedAgentDataRepository = new ProcessedAgentDataRepository(dataContext.ProcessedAgentDatas);
        }

        public ProcessedAgentDataRepository ProcessedAgentDataRepository { get; }
    }
}