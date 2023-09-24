using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ZetaTradingTask.Application.Abstractions;
using ZetaTradingTask.Common.Models.Requests;

namespace ZetaTradingTask.WebApi.Controllers
{
    [Route("node")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly INodeService _nodeService;

        public NodeController(INodeService nodeService)
        {
            _nodeService = nodeService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [Required] string treeName,
            [Required] long parentNodeId,
            [Required] string nodeName)
        {
            await _nodeService.Create(new CreateNodeRequest
            {
                TreeName = treeName,
                ParentNodeId = parentNodeId,
                NodeName = nodeName,
            });

            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(
            [Required] string treeName,
            [Required] long nodeId)
        {
            await _nodeService.Delete(new DeleteNodeRequest
            { 
                TreeName = treeName,
                NodeId = nodeId,
            });

            return Ok();
        }

        [HttpPost("rename")]
        public async Task<IActionResult> Rename(
            [Required] string treeName,
            [Required] long nodeId,
            [Required] string newNodeName)
        {
            await _nodeService.Rename(new RenameNodeRequest
            {
                TreeName = treeName,
                NodeId = nodeId,
                NewNodeName = newNodeName,
            });

            return Ok();
        }
    }
}
