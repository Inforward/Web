using System;
using Newtonsoft.Json;

namespace Bespoke.Services.Messages.Blog
{
    [Serializable]
    public abstract class BaseResponse
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "count_total")]
        public int TotalCount { get; set; }

        [JsonProperty(PropertyName = "pages")]
        public int Pages { get; set; }

        public int Page { get; set; }
    }
}