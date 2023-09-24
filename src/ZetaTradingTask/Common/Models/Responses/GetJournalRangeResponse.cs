using ZetaTradingTask.Common.Models.Dto;

namespace ZetaTradingTask.Common.Models.Responses
{
    public class GetJournalRangeResponse
    {
        public int Count { get; init; }
        public required List<JournalDto> Items { get; init; }
    }
}
