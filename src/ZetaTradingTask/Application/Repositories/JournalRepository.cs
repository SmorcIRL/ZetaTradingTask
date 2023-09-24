using Microsoft.EntityFrameworkCore;
using ZetaTradingTask.Application.Abstractions;
using ZetaTradingTask.Database;
using ZetaTradingTask.Database.Entities;

namespace ZetaTradingTask.Application.Repositories
{
    public class JournalRepository : RepositoryBase<JournalEntity>, IJournalRepository
    {
        public JournalRepository(AppDbContext context) : base(context)
        {
        }

        public JournalEntity Create(JournalEntity entity)
        {
            Context.Journal.Add(entity);
            return entity;
        }

        public async Task<JournalEntity?> FindById(long id)
        {
            return await AsQueryable()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<JournalEntity>> Filter(
            int skip,
            int take,
            DateTimeOffset? from,
            DateTimeOffset? to,
            string? search)
        {
            var query = AsQueryable();

            if (from != null)
            {
                query = query.Where(x => x.CreatedAt >= from);
            }

            if (to != null)
            {
                query = query.Where(x => x.CreatedAt <= to);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.ExceptionType.Contains(search));
            }

            return await query
                .OrderBy(x => x.CreatedAt)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }
    }
}
