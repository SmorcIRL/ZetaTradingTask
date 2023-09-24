using Microsoft.EntityFrameworkCore;
using ZetaTradingTask.Database.Configuration;
using ZetaTradingTask.Database.Entities;

namespace ZetaTradingTask.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual required DbSet<NodeEntity> Nodes { get; init; }
        public virtual required DbSet<JournalEntity> Journal { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new NodeEntityConfiguration());
            builder.ApplyConfiguration(new JournalEntityConfiguration());
        }
    }
}
