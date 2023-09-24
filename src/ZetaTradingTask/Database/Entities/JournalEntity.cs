namespace ZetaTradingTask.Database.Entities
{
    public class JournalEntity
    {
        public long Id { get; init; }
        public Guid EventId { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public required string ExceptionType { get; init; }
        public required string ExceptionMessage { get; init; }
        public required string ExceptionStackTrace { get; init; }
        public required string Request { get; init; }
    }
}
