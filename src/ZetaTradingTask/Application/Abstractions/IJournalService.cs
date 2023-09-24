using ZetaTradingTask.Common.Models.Requests;
using ZetaTradingTask.Common.Models.Responses;

namespace ZetaTradingTask.Application.Abstractions
{
    public interface IJournalService
    {
        Task<GetJournalRangeResponse> GetRange(GetJournalRangeRequest request);
        Task<GetJournalSingleResponse> GetSingle(GetJournalSingleRequest request);
    }
}
