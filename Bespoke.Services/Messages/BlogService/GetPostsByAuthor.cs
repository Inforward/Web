using System;
using Bespoke.Models.Blog;
using Newtonsoft.Json;

namespace Bespoke.Services.Messages.Blog
{
    [Serializable]
    public class GetPostsByAuthor : GetPostsResponse
    {
        [JsonProperty(PropertyName = "author")]
        public Author Author { get; set; }
    }
}
