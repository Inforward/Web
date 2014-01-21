using System;
using System.Collections.Generic;
using Bespoke.Models.Blog;
using Newtonsoft.Json;

namespace Bespoke.Services.Messages.Blog
{
    [Serializable]
    public class GetPostsByTagResponse : GetPostsResponse
    {
        [JsonProperty(PropertyName = "tag")]
        public Tag Tag { get; set; }
    }
}