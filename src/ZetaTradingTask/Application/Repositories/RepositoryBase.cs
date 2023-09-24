using ZetaTradingTask.Application.Abstractions;
using ZetaTradingTask.Database;

namespace ZetaTradingTask.Application.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext Context;

        protected RepositoryBase(AppDbContext context)
        {
            Context = context;
        }

        public IQueryable<T> AsQueryable()
        {
            return Context.Set<T>().AsQueryable();
        }
    }
}
