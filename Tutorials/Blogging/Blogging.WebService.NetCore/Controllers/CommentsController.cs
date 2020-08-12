﻿using System;
using JsonApiFramework.JsonApi;
using JsonApiFramework.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blogging.WebService.Controllers
{
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentsController(ILogger<CommentsController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        #region WebApi Methods
        [HttpGet("comments")]
        public Document GetCollection()
        {
            /////////////////////////////////////////////////////
            // Get all Comments from repository
            /////////////////////////////////////////////////////
            var comments = BloggingRepository.GetComments();

            /////////////////////////////////////////////////////
            // Build JSON API document
            /////////////////////////////////////////////////////
            var displayUrl = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();
            var currentRequestUri = new Uri(displayUrl);
            using var documentContext = new BloggingDocumentContext(currentRequestUri);
            var document = documentContext
                .NewDocument(currentRequestUri)
                    .SetJsonApiVersion(JsonApiVersion.Version10)
                    .Links()
                        .AddUpLink()
                        .AddSelfLink()
                    .LinksEnd()
                    .ResourceCollection(comments)
                        .Relationships()
                            .AddRelationship("article", new[] { Keywords.Related })
                            .AddRelationship("author", new[] { Keywords.Related })
                        .RelationshipsEnd()
                        .Links()
                            .AddSelfLink()
                        .LinksEnd()
                    .ResourceCollectionEnd()
                .WriteDocument();

            return document;
        }

        [HttpGet("comments/{id}")]
        public Document Get(string id)
        {
            /////////////////////////////////////////////////////
            // Get Comment by identifier from repository
            /////////////////////////////////////////////////////
            var comment = BloggingRepository.GetComment(Convert.ToInt64(id));

            /////////////////////////////////////////////////////
            // Build JSON API document
            /////////////////////////////////////////////////////
            var displayUrl = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();
            var currentRequestUri = new Uri(displayUrl);
            using var documentContext = new BloggingDocumentContext(currentRequestUri);
            var document = documentContext
                .NewDocument(currentRequestUri)
                    .SetJsonApiVersion(JsonApiVersion.Version10)
                    .Links()
                        .AddUpLink()
                        .AddSelfLink()
                    .LinksEnd()
                    .Resource(comment)
                        .Relationships()
                            .AddRelationship("article", new[] { Keywords.Related })
                            .AddRelationship("author", new[] { Keywords.Related })
                        .RelationshipsEnd()
                        .Links()
                            .AddSelfLink()
                        .LinksEnd()
                    .ResourceEnd()
                .WriteDocument();

            return document;
        }

        [HttpGet("comments/{id}/article")]
        public Document GetCommentToArticle(string id)
        {
            /////////////////////////////////////////////////////
            // Get Comment to related Article by Comment identifier from repository
            /////////////////////////////////////////////////////
            var commentToArticle = BloggingRepository.GetCommentToArticle(Convert.ToInt64(id));

            /////////////////////////////////////////////////////
            // Build JSON API document
            /////////////////////////////////////////////////////
            var displayUrl = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();
            var currentRequestUri = new Uri(displayUrl);
            using var documentContext = new BloggingDocumentContext(currentRequestUri);
            var document = documentContext
                .NewDocument(currentRequestUri)
                    .SetJsonApiVersion(JsonApiVersion.Version10)
                    .Links()
                        .AddUpLink()
                        .AddSelfLink()
                    .LinksEnd()
                    .Resource(commentToArticle)
                        .Relationships()
                            .AddRelationship("blog", new[] { Keywords.Related })
                            .AddRelationship("author", new[] { Keywords.Related })
                            .AddRelationship("comments", new[] { Keywords.Related })
                        .RelationshipsEnd()
                        .Links()
                            .AddSelfLink()
                        .LinksEnd()
                    .ResourceEnd()
                .WriteDocument();

            return document;
        }

        [HttpGet("comments/{id}/author")]
        public Document GetCommentToAuthor(string id)
        {
            /////////////////////////////////////////////////////
            // Get Comment to related Author by Comment identifier from repository
            /////////////////////////////////////////////////////
            var commentToAuthor = BloggingRepository.GetCommentToAuthor(Convert.ToInt64(id));

            /////////////////////////////////////////////////////
            // Build JSON API document
            /////////////////////////////////////////////////////
            var displayUrl = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();
            var currentRequestUri = new Uri(displayUrl);
            using var documentContext = new BloggingDocumentContext(currentRequestUri);
            var document = documentContext
                .NewDocument(currentRequestUri)
                    .SetJsonApiVersion(JsonApiVersion.Version10)
                    .Links()
                        .AddUpLink()
                        .AddSelfLink()
                    .LinksEnd()
                    .Resource(commentToAuthor)
                        .Relationships()
                            .AddRelationship("articles", new[] { Keywords.Related })
                            .AddRelationship("comments", new[] { Keywords.Related })
                        .RelationshipsEnd()
                        .Links()
                            .AddSelfLink()
                        .LinksEnd()
                    .ResourceEnd()
                .WriteDocument();

            return document;
        }

        [HttpPost("comments")]
        public Document Post([FromBody]Document inDocument)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("comments/{id}")]
        public Document Patch(string id, [FromBody]Document inDocument)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("comments/{id}")]
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}