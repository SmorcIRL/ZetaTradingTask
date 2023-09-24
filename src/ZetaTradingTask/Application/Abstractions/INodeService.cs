using ZetaTradingTask.Common.Models.Requests;

namespace ZetaTradingTask.Application.Abstractions
{
    public interface INodeService
    {
        Task Create(CreateNodeRequest request);
        Task Delete(DeleteNodeRequest request);
        Task Rename(RenameNodeRequest request);
    }
}
