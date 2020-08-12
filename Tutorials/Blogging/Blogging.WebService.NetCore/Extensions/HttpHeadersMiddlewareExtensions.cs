using Blogging.WebService.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Blogging.WebService.Extensions
{
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HttpHeadersMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpHeadersMiddleware(this IApplicationBuilder builder)
        {
            var httpHeadersPolicy = new HttpHeadersBuilder()
                .AddJsonApiMimeTypePolicy()
                .Build();

            return builder.UseMiddleware<HttpHeadersMiddleware>(httpHeadersPolicy);
        }
    }
}
