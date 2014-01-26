using System;
using System.Collections.Generic;
using Bespoke.Models.Blog;
using Newtonsoft.Json;

namespace Bespoke.Services.Messages.Blog
{
    [Serializable]
    public class GetPostsByCategoryResponse : GetPostsResponse
    {
        [JsonProperty(PropertyName = "category")]
        public Category Category { get; set; }
    }
}