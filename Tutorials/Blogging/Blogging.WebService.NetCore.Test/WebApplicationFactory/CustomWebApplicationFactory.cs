using Microsoft.AspNetCore.Mvc.Testing;

namespace Blogging.WebService.NetCore.Test.WebApplicationFactory
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {

    }
}
