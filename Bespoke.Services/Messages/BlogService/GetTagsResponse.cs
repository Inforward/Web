using System;
using System.Collections.Generic;
using Bespoke.Models.Blog;
using Newtonsoft.Json;

namespace Bespoke.Services.Messages.Blog
{
    [Serializable]
    public class GetTagsResponse : BaseResponse
    {
        [JsonProperty(PropertyName = "tags")]
        public List<Tag> Tags { get; set; }
    }
}