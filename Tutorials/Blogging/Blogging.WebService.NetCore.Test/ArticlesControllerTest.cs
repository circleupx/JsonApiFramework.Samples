using Blogging.WebService.NetCore.Test.WebApplicationFactory;
using Xunit;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using Blogging.ServiceModel;
using Exceptionless;
using System.Net.Http;
using System.Text;
using System.Net;
using FluentAssertions;
using JsonApiFramework;
using System;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Blogging.WebService.NetCore.Test
{
    public class ArticlesControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        public ArticlesControllerTest(CustomWebApplicationFactory<Program> customWebApplicationFactory)
        {
            _customWebApplicationFactory = customWebApplicationFactory;
        }

        private readonly WebApplicationFactory<Program> _customWebApplicationFactory;

        [Fact]
        public async Task Post_CreateNewArticle_ShouldReturnCreatedStatusCode()
        {
            // Arrange
            var httpStatusCode = HttpStatusCode.OK;
            using var httpClient = _customWebApplicationFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.JsonApiMimeType));

            // Act
            var requestUri = httpClient.BaseAddress.AbsoluteUri;
            var homeDocumentResponse = await httpClient.GetStringAsync(requestUri);
            var homeDocument = await JsonObject.ParseAsync<Document>(homeDocumentResponse);

            var hasLinkToArticlesResource = homeDocument.GetResource().TryGetLink("articles", out var linkToArticlesResource);
            if (hasLinkToArticlesResource) 
            {
                var article = new Article
                {
                    Title = RandomData.GetTitleWords(),
                    Text = RandomData.GetParagraphs()
                };

                using var bloggingDocumentContext = new BloggingClientDocumentContext();
                var document = bloggingDocumentContext
                    .NewDocument()
                        .Resource(article)
                            .Relationships()
                                .AddRelationship("author", ToOneResourceLinkage.Create("9"))
                                .AddRelationship("blog", ToOneResourceLinkage.Create("1"))
                            .RelationshipsEnd()
                        .ResourceEnd()
                    .WriteDocument();

                var content = new StringContent(await document.ToJsonAsync(), Encoding.UTF8, Constants.JsonApiMimeType);
                var httpResponseMessage = await httpClient.PostAsync(linkToArticlesResource, content);
                httpStatusCode = httpResponseMessage.StatusCode;
            }

            // Assert
            httpStatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Post_CreateNewArticle_ShouldHaveLocationHeader()
        {
            // Arrange
            Uri locationHeader = null;
            using var httpClient = _customWebApplicationFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.JsonApiMimeType));

            // Act
            var requestUri = httpClient.BaseAddress.AbsoluteUri;
            var homeDocumentResponse = await httpClient.GetStringAsync(requestUri);
            var homeDocument = await JsonObject.ParseAsync<Document>(homeDocumentResponse);

            var hasLinkToArticlesResource = homeDocument.GetResource().TryGetLink("articles", out var linkToArticlesResource);
            if (hasLinkToArticlesResource)
            {
                var article = new Article
                {
                    Title = RandomData.GetTitleWords(),
                    Text = RandomData.GetParagraphs()
                };

                using var bloggingDocumentContext = new BloggingClientDocumentContext();
                var document = bloggingDocumentContext
                    .NewDocument()
                        .Resource(article)
                            .Relationships()
                                .AddRelationship("author", ToOneResourceLinkage.Create("9"))
                                .AddRelationship("blog", ToOneResourceLinkage.Create("1"))
                            .RelationshipsEnd()
                        .ResourceEnd()
                    .WriteDocument();

                var content = new StringContent(await document.ToJsonAsync(), Encoding.UTF8, Constants.JsonApiMimeType);
                var httpResponseMessage = await httpClient.PostAsync(linkToArticlesResource, content);
                locationHeader = httpResponseMessage.Headers.Location;
            }

            // Assert
            locationHeader.Should().NotBeNull();
        }

        [Fact]
        public async Task Post_CreateNewArticle_ShouldHaveJsonApiMimeType()
        {
            // Arrange
            string responseMimeType = string.Empty;
            using var httpClient = _customWebApplicationFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.JsonApiMimeType));

            // Act
            var requestUri = httpClient.BaseAddress.AbsoluteUri;
            var homeDocumentResponse = await httpClient.GetStringAsync(requestUri);
            var homeDocument = await JsonObject.ParseAsync<Document>(homeDocumentResponse);

            var hasLinkToArticlesResource = homeDocument.GetResource().TryGetLink("articles", out var linkToArticlesResource);
            if (hasLinkToArticlesResource)
            {
                var article = new Article
                {
                    Title = RandomData.GetTitleWords(),
                    Text = RandomData.GetParagraphs()
                };

                using var bloggingDocumentContext = new BloggingClientDocumentContext();
                var document = bloggingDocumentContext
                    .NewDocument()
                        .Resource(article)
                            .Relationships()
                                .AddRelationship("author", ToOneResourceLinkage.Create("9"))
                                .AddRelationship("blog", ToOneResourceLinkage.Create("1"))
                            .RelationshipsEnd()
                        .ResourceEnd()
                    .WriteDocument();

                var content = new StringContent(await document.ToJsonAsync(), Encoding.UTF8, Constants.JsonApiMimeType);
                var httpResponseMessage = await httpClient.PostAsync(linkToArticlesResource, content);
                responseMimeType = httpResponseMessage.Content.Headers.ContentType.MediaType;
            }

            // Assert
            responseMimeType.Should().BeEquivalentTo(Constants.JsonApiMimeType);
        }
    }
}
