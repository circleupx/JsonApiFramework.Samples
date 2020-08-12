using Blogging.ServiceModel;
using JsonApiFramework;
using JsonApiFramework.Client;
using JsonApiFramework.JsonApi;

namespace Blogging.WebService.NetCore.Test
{
    class BloggingClientDocumentContext : DocumentContext
    {
        public BloggingClientDocumentContext(Document document) : base(document)
        {

        }

        public BloggingClientDocumentContext()
        {

        }

        protected override void OnConfiguring(IDocumentContextOptionsBuilder optionsBuilder)
        {
            var conventions = ConfigurationFactory.CreateConventions();
            var serviceModel = ConfigurationFactory.CreateServiceModel();

            optionsBuilder.UseConventions(conventions);
            optionsBuilder.UseServiceModel(serviceModel);
        }
    }
}
