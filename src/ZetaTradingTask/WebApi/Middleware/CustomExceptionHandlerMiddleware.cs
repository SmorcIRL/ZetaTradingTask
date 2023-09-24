using System.Net;
using System.Text;
using Newtonsoft.Json;
using ZetaTradingTask.Application.Abstractions;
using ZetaTradingTask.Common.Exceptions;
using ZetaTradingTask.Database.Entities;

namespace ZetaTradingTask.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJournalRepository journalRepository, IUnitOfWork unitOfWork)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex, journalRepository, unitOfWork);
            }
        }

        private static async Task HandleException(HttpContext context, Exception exception, IJournalRepository journalRepository, IUnitOfWork unitOfWork)
        {
            var eventId = Guid.NewGuid();

            var entity = new JournalEntity
            {
                EventId = eventId,
                CreatedAt = DateTimeOffset.Now,
                ExceptionType = exception.GetType().Name,
                ExceptionMessage = exception is SecureException
                    ? exception.Message
                    : $"Internal server error ID = {eventId}",
                ExceptionStackTrace = exception.StackTrace ?? string.Empty,
                Request = JsonConvert.SerializeObject(new
                {
                    Path = context.Request.Path.ToString(),
                    Query = context.Request.QueryString.ToString(),
                    Body = await GetRawBodyAsync(context.Request),
                }),
            };

            entity = journalRepository.Create(entity);

            await unitOfWork.SaveChangesAsync();

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var responseContent = new
            {
                Id = entity.EventId,
                Type = entity.ExceptionType,
                Data = new
                {
                    Message = entity.ExceptionMessage,
                },
            };

            await response.WriteAsync(JsonConvert.SerializeObject(responseContent));
        }

        public static async Task<string?> GetRawBodyAsync(HttpRequest request)
        {
            if (request.Method != HttpMethods.Post || request is not { ContentLength: > 0 })
            {
                return null;
            }

            request.Body.Position = 0;
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            _ = await request.Body.ReadAsync(buffer);
            var requestContent = Encoding.UTF8.GetString(buffer);

            return requestContent;
        }
    }
}