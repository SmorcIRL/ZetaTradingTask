using ZetaTradingTask.Database.Entities;

namespace ZetaTradingTask.Application.Abstractions
{
    public interface IJournalRepository : IRepository<JournalEntity>
    {
        JournalEntity Create(JournalEntity entity);

        Task<JournalEntity?> FindById(long id);

        Task<List<JournalEntity>> Filter(
            int skip,
            int take,
            DateTimeOffset? from,
            DateTimeOffset? to,
            string? search);
    }
}
