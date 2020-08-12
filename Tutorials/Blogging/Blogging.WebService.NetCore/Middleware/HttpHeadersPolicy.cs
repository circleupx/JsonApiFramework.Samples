using System.Collections.Generic;

namespace Blogging.WebService.Middleware
{
    public class HttpHeadersPolicy 
    {
        public IDictionary<string, string> SetHeaders { get; } = new Dictionary<string, string>();
        public ISet<string> RemoveHeaders { get; } = new HashSet<string>();
    }
}
