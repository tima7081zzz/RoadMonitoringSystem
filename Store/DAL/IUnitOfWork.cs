namespace Store.DAL
{
    public interface IUnitOfWork
    {
        ProcessedAgentDataRepository ProcessedAgentDataRepository { get; }
    }
}