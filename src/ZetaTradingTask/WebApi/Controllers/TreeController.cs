using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ZetaTradingTask.Application.Abstractions;
using ZetaTradingTask.Common.Models.Requests;
using ZetaTradingTask.Common.Models.Responses;

namespace ZetaTradingTask.WebApi.Controllers
{
    [Route("tree")]
    [ApiController]
    public class TreeController : ControllerBase
    {
        private readonly ITreeService _treeService;

        public TreeController(ITreeService treeService)
        {
            _treeService = treeService;
        }

        [HttpPost("get")]
        public async Task<GetTreeResponse> Get([Required] string treeName)
        {
            return await _treeService.GetOrCreate(new GetTreeRequest
            {
                TreeName = treeName,
            });
        }
    }
}
