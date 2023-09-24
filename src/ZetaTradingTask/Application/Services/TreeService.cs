using ZetaTradingTask.Application.Abstractions;
using ZetaTradingTask.Common.Models.Dto;
using ZetaTradingTask.Common.Models.Requests;
using ZetaTradingTask.Common.Models.Responses;
using ZetaTradingTask.Database.Entities;

namespace ZetaTradingTask.Application.Services
{
    public class TreeService : ITreeService
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TreeService(IUnitOfWork unitOfWork, INodeRepository nodeRepository)
        {
            _unitOfWork = unitOfWork;
            _nodeRepository = nodeRepository;
        }

        public async Task<GetTreeResponse> GetOrCreate(GetTreeRequest request)
        {
            var nodes = await _nodeRepository.List(request.TreeName);

            if (nodes is not { Count: > 0 })
            {
                var newRoot = _nodeRepository.Create(new NodeEntity
                {
                    Name = request.TreeName,
                    TreeName = request.TreeName,
                });

                await _unitOfWork.SaveChangesAsync();

                return new GetTreeResponse
                {
                    Id = newRoot.Id,
                    Name = newRoot.Name,
                    Children = new List<TreeDto>(),
                };
            }

            var rootNode = nodes.Single(x => x.ParentNodeId == null);

            var parentIdGrouping = nodes.ToLookup(x => x.ParentNodeId);
            var queue = new Queue<NodeEntity>(parentIdGrouping[rootNode.Id]);
            var dtoMapping = new Dictionary<long, TreeDto>
            {
                [rootNode.Id] = new TreeDto
                {
                    Id = rootNode.Id,
                    Name = rootNode.Name,
                    Children = new List<TreeDto>(),
                },
            };

            while (queue.Any())
            {
                var current = queue.Dequeue();

                var dto = new TreeDto
                {
                    Id = current.Id,
                    Name = current.Name,
                    Children = new List<TreeDto>(),
                };

                dtoMapping[current.Id] = dto;
                dtoMapping[current.ParentNodeId!.Value].Children.Add(dto);

                foreach (var entity in parentIdGrouping[current.Id])
                {
                    queue.Enqueue(entity);
                }
            }

            var rootDto = dtoMapping[rootNode.Id];

            return new GetTreeResponse
            {
                Id = rootDto.Id,
                Name = rootDto.Name,
                Children = rootDto.Children,
            };
        }
    }
}
