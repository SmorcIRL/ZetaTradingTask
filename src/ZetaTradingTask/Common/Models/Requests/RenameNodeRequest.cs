namespace ZetaTradingTask.Common.Models.Requests
{
    public class RenameNodeRequest
    {
        public required string TreeName { get; init; }
        public long NodeId { get; init; }
        public required string NewNodeName { get; init; }
    }
}
