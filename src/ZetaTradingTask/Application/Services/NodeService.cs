using ZetaTradingTask.Application.Abstractions;
using ZetaTradingTask.Common.Exceptions;
using ZetaTradingTask.Common.Models.Requests;
using ZetaTradingTask.Database.Entities;

namespace ZetaTradingTask.Application.Services
{
    public class NodeService : INodeService
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NodeService(IUnitOfWork unitOfWork, INodeRepository nodeRepository)
        {
            _unitOfWork = unitOfWork;
            _nodeRepository = nodeRepository;
        }

        public async Task Create(CreateNodeRequest request)
        {
            var parentNode = await _nodeRepository.FindById(request.TreeName, request.ParentNodeId);
            if (parentNode == default)
            {
                throw new SecureException($"Parent node not found, id = {request.ParentNodeId}");
            }

            var isNameOccupied = await _nodeRepository.IsNameOccupied(request.TreeName, request.NodeName);
            if (isNameOccupied)
            {
                throw new SecureException($"Name {request.NodeName} is occupied for tree {request.TreeName}");
            }
            
            _ = _nodeRepository.Create(new NodeEntity
            { 
                ParentNodeId = parentNode.Id,
                TreeName = parentNode.TreeName,
                Name = request.NodeName,
            });

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(DeleteNodeRequest request)
        {
            var node = await _nodeRepository.FindById(request.TreeName, request.NodeId);
            if (node == default)
            {
                throw new SecureException($"Node not found, id = {request.NodeId}");
            }

            var haveChildren = await _nodeRepository.HasChildren(request.TreeName, request.NodeId);
            if (haveChildren)
            {
                throw new SecureException($"Node has children nodes, id = {request.NodeId}");
            }

            _nodeRepository.Delete(node);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Rename(RenameNodeRequest request)
        {
            var node = await _nodeRepository.FindById(request.TreeName, request.NodeId);
            if (node == default)
            {
                throw new SecureException($"Node not found, id = {request.NodeId}");
            }

            var isNameOccupied = await _nodeRepository.IsNameOccupied(request.TreeName, request.NewNodeName);
            if (isNameOccupied)
            {
                throw new SecureException($"Name {request.NewNodeName} is occupied for tree {request.TreeName}");
            }

            node.Name = request.NewNodeName;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
