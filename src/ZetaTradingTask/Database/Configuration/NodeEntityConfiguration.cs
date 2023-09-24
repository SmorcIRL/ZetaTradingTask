using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZetaTradingTask.Database.Entities;

namespace ZetaTradingTask.Database.Configuration
{
    public class NodeEntityConfiguration : IEntityTypeConfiguration<NodeEntity>
    {
        public void Configure(EntityTypeBuilder<NodeEntity> builder)
        {
            builder
                .ToTable("Node");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder
                .Property(x => x.TreeName)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder
                .HasIndex(x => new { x.Name, x.TreeName })
                .IsUnique();

            builder
                .HasOne(x => x.ParentNode)
                .WithMany()
                .HasForeignKey(x => x.ParentNodeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
