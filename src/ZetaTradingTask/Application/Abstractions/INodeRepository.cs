using ZetaTradingTask.Database.Entities;

namespace ZetaTradingTask.Application.Abstractions
{
    public interface INodeRepository : IRepository<NodeEntity>
    {
        Task<List<NodeEntity>> List(string tree);
        NodeEntity Create(NodeEntity entity);
        void Delete(NodeEntity entity);
        Task<NodeEntity?> FindById(string tree, long id);
        Task<bool> HasChildren(string tree, long id);
        Task<bool> IsNameOccupied(string tree, string name);
    }
}
