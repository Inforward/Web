using System;
using Bespoke.Models.Blog;
using Newtonsoft.Json;

namespace Bespoke.Services.Messages.Blog
{
    [Serializable]
    public class GetPostResponse : BaseResponse
    {
        [JsonProperty(PropertyName = "next_url")]
        public string NextUrl { get; set; }

        [JsonProperty(PropertyName = "post")]
        public Post Post { get; set; }
    }
}