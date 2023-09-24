namespace ZetaTradingTask.Common.Models.Responses
{
    public class GetJournalSingleResponse
    {
        public long Id { get; init; }
        public Guid EventId { get; init; }
        public required string Text { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }
}
