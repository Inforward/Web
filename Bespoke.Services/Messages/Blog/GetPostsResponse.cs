using System;
using System.Collections.Generic;
using Bespoke.Models.Blog;
using Newtonsoft.Json;

namespace Bespoke.Services.Messages.Blog
{
    [Serializable]
    public class GetPostsResponse : BaseResponse
    {
        [JsonProperty(PropertyName = "posts")]
        public List<Post> Posts { get; set; }
    }
}