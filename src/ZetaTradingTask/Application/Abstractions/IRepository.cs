namespace ZetaTradingTask.Application.Abstractions
{
    public interface IRepository<T>
    {
        IQueryable<T> AsQueryable();
    }
}
