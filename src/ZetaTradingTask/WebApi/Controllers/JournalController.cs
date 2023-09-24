using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ZetaTradingTask.Application.Abstractions;
using ZetaTradingTask.Common.Models.Requests;
using ZetaTradingTask.Common.Models.Responses;

namespace ZetaTradingTask.WebApi.Controllers
{
    [Route("journal")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _journalService;

        public JournalController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        [HttpPost("getRange")]
        public async Task<GetJournalRangeResponse> GetRange(
            int skip = 0,
            int take = 50,
            GetJournalRangeFilter? filter = null)
        {
            return await _journalService.GetRange(new GetJournalRangeRequest
            {
                Skip = skip,
                Take = take,
                Filter = filter,
            });
        }

        [HttpPost("getSingle")]
        public async Task<GetJournalSingleResponse> GetSingle(
            [Required] long id)
        {
            return await _journalService.GetSingle(new GetJournalSingleRequest
            {
                Id = id,
            });
        }
    }
}
