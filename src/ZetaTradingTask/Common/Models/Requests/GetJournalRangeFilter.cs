namespace ZetaTradingTask.Common.Models.Requests
{
    public class GetJournalRangeFilter
    {
        public DateTimeOffset? From { get; init; }
        public DateTimeOffset? To { get; init; }
        public string? Search { get; init; }
    }
}
