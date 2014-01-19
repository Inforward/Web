using Newtonsoft.Json;

namespace Bespoke.Models.Blog
{
    public class Tag
    {
        [JsonProperty(PropertyName = "id")]
        public int TagId { get; set; }

        [JsonProperty(PropertyName = "post_count")]
        public int PostCount { get; set; }

        [JsonProperty(PropertyName = "slug")]
        public string Slug { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}