using ZetaTradingTask.Common.Models.Requests;
using ZetaTradingTask.Common.Models.Responses;

namespace ZetaTradingTask.Application.Abstractions
{
    public interface ITreeService
    {
        Task<GetTreeResponse> GetOrCreate(GetTreeRequest request);
    }
}
