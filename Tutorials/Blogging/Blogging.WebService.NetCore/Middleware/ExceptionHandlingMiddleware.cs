using JsonApiFramework.JsonApi;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Blogging.WebService.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleException(httpContext, exception);
            }
        }

        private Task HandleException(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorsDocument = new ErrorsDocument();
            var eventTitle = exception.GetType().Name;
            var randomEventId = RandomNumberGenerator.GetInt32(0, 10000);
            var eventId = new EventId(randomEventId, eventTitle);
            var errorException = new ErrorException(Error.CreateId(eventId.ToString()), HttpStatusCode.InternalServerError, Error.CreateNewId(), eventTitle, exception.Message, exception.InnerException);

            errorsDocument.AddError(errorException);
            return httpContext.Response.WriteAsync(errorsDocument.ToJson());
        }
    }
}
