using System;
using Newtonsoft.Json;

namespace Bespoke.Models.Blog
{
    [Serializable]
    public class Category
    {
        [JsonProperty(PropertyName = "id")]
        public int CategoryId { get; set; }

        [JsonProperty(PropertyName = "parent")]
        public int ParentCategoryId { get; set; }

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