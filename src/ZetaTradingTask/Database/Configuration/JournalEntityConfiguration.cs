using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZetaTradingTask.Database.Entities;

namespace ZetaTradingTask.Database.Configuration
{
    public class JournalEntityConfiguration : IEntityTypeConfiguration<JournalEntity>
    {
        public void Configure(EntityTypeBuilder<JournalEntity> builder)
        {
            builder
                .ToTable("Journal");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.ExceptionType)
                .HasColumnType("varchar(1024)")
                .IsRequired();

            builder
                .Property(x => x.ExceptionMessage)
                .IsRequired();

            builder
                .Property(x => x.ExceptionStackTrace)
                .IsRequired();

            builder
                .Property(x => x.Request)
                .IsRequired();

            builder
                .HasIndex(x => x.EventId)
                .IsUnique();

            builder
                .HasIndex(x => x.ExceptionType);
        }
    }
}
