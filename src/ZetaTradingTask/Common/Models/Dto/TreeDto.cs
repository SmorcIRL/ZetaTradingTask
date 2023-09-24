namespace ZetaTradingTask.Common.Models.Dto
{
    public class TreeDto
    {
        public long Id { get; init; }
        public required string Name { get; init; }
        public required List<TreeDto> Children { get; init; }
    }
}
