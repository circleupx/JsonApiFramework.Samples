using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Blogging.WebService.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class HttpHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpHeadersPolicy _httpHeadersPolicy;

        public HttpHeadersMiddleware(RequestDelegate next, HttpHeadersPolicy httpHeadersPolicy)
        {
            _next = next;
            _httpHeadersPolicy = httpHeadersPolicy;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var headers = httpContext.Response.Headers;
            foreach (var (key, value) in _httpHeadersPolicy.SetHeaders) headers[key] = value;
            foreach (var key in _httpHeadersPolicy.RemoveHeaders) headers.Remove(key);

            await _next(httpContext);
        }
    }
}
