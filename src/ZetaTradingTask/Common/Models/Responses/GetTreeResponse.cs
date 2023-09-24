using ZetaTradingTask.Common.Models.Dto;

namespace ZetaTradingTask.Common.Models.Responses
{
    public class GetTreeResponse
    {
        public long Id { get; init; }
        public required string Name { get; init; }
        public required List<TreeDto> Children { get; init; }
    }
}
