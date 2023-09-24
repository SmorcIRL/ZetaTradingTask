using ZetaTradingTask.Application.Abstractions;
using ZetaTradingTask.Common.Exceptions;
using ZetaTradingTask.Common.Models.Dto;
using ZetaTradingTask.Common.Models.Requests;
using ZetaTradingTask.Common.Models.Responses;

namespace ZetaTradingTask.Application.Services
{
    public class JournalService : IJournalService
    {
        private readonly IJournalRepository _journalRepository;

        public JournalService(IJournalRepository journalRepository)
        {
            _journalRepository = journalRepository;
        }

        public async Task<GetJournalRangeResponse> GetRange(GetJournalRangeRequest request)
        {
            var records = await _journalRepository.Filter
            (
                request.Skip,
                request.Take,
                request.Filter?.From,
                request.Filter?.To,
                request.Filter?.Search
            );

            return new GetJournalRangeResponse
            { 
                Count = records.Count,
                Items = records.Select(x => new JournalDto
                {
                    Id = x.Id,
                    EventId = x.EventId,
                    CreatedAt = x.CreatedAt,
                }).ToList(),
            };
        }

        public async Task<GetJournalSingleResponse> GetSingle(GetJournalSingleRequest request)
        {
            var entity = await _journalRepository.FindById(request.Id);
            if (entity == default)
            {
                throw new SecureException($"Journal record not found, id = {request.Id}");
            }

            return new GetJournalSingleResponse
            {
                Id = entity.Id,
                EventId = entity.EventId,
                CreatedAt = entity.CreatedAt,
                Text = entity.ExceptionMessage,
            };
        }
    }
}
