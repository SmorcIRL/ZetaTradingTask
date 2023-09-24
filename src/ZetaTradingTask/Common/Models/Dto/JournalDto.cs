namespace ZetaTradingTask.Common.Models.Dto
{
    public class JournalDto
    {
        public long Id { get; init; }
        public Guid EventId { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }
}
