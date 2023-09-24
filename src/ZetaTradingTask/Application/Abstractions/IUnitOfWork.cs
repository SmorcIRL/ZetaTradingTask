namespace ZetaTradingTask.Application.Abstractions
{
    public interface IUnitOfWork
    {
        public Task SaveChangesAsync();
    }
}
