namespace ZetaTradingTask.Database.Entities
{
    public class NodeEntity
    {
        public long Id { get; init; }
        public required string Name { get; set; }
        public required string TreeName { get; init; }
        public long? ParentNodeId { get; init; }
        public NodeEntity? ParentNode { get; } = null!;
    }
}
