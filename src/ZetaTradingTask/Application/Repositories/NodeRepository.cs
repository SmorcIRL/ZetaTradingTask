using Microsoft.EntityFrameworkCore;
using ZetaTradingTask.Application.Abstractions;
using ZetaTradingTask.Database;
using ZetaTradingTask.Database.Entities;

namespace ZetaTradingTask.Application.Repositories
{
    public class NodeRepository : RepositoryBase<NodeEntity>, INodeRepository
    {
        public NodeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<NodeEntity>> List(string tree)
        {
            return await AsQueryable()
                .Where(x => x.TreeName == tree)
                .ToListAsync();
        }

        public NodeEntity Create(NodeEntity entity)
        {
            Context.Nodes.Add(entity);
            return entity;
        }

        public void Delete(NodeEntity entity)
        {
            Context.Nodes.Remove(entity);
        }

        public async Task<NodeEntity?> FindById(string tree, long id)
        {
            return await AsQueryable()
                .Where(x => x.TreeName == tree)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> HasChildren(string tree, long id)
        {
            return await AsQueryable()
                .Where(x => x.TreeName == tree)
                .Where(x => x.ParentNodeId == id)
                .AnyAsync();
        }

        public async Task<bool> IsNameOccupied(string tree, string name)
        {
            return await AsQueryable()
                .Where(x => x.TreeName == tree)
                .Where(x => x.Name == name)
                .AnyAsync();
        }
    }
}
