namespace ZetaTradingTask.Common.Models.Requests
{
    public class GetJournalRangeRequest
    {
        public int Skip { get; init; }
        public int Take { get; init; }
        public GetJournalRangeFilter? Filter { get; init; }
    }
}
