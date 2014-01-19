using Newtonsoft.Json;

namespace Bespoke.Models.Blog
{
    public class Author
    {
        [JsonProperty(PropertyName = "id")]
        public int AuthorId { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "nickname")]
        public string NickName { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "slug")]
        public string Slug { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}