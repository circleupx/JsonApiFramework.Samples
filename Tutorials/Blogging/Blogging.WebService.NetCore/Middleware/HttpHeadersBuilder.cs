namespace Blogging.WebService.Middleware
{
    public class HttpHeadersBuilder
    {
        private readonly HttpHeadersPolicy _httpHeadersPolicy = new HttpHeadersPolicy();

        public HttpHeadersBuilder AddJsonApiMimeTypePolicy()
        {
            _httpHeadersPolicy.SetHeaders[Constants.ContentType] = Constants.JsonApiMimeType;
            return this;
        }

        public HttpHeadersPolicy Build() 
        {
            return _httpHeadersPolicy;
        }
    }
}
