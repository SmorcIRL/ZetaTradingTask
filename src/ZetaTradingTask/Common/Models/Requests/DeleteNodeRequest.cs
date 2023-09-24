namespace ZetaTradingTask.Common.Models.Requests
{
    public class DeleteNodeRequest
    {
        public required string TreeName { get; init; }
        public long NodeId { get; init; }
    }
}
