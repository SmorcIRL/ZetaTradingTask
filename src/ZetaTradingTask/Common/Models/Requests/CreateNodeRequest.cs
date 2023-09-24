namespace ZetaTradingTask.Common.Models.Requests
{
    public class CreateNodeRequest
    {
        public required string TreeName { get; init; }
        public long ParentNodeId { get; init; }
        public required string NodeName { get; init; }
    }
}
