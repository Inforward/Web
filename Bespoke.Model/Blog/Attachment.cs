using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bespoke.Models.Blog
{
    public class Attachment
    {
        public Attachment()
        {
            Images = new Images();
        }

        [JsonProperty(PropertyName = "id")]
        public int AttachmentId { get; set; }

        [JsonProperty(PropertyName = "parent")]
        public int AttachmentParentId { get; set; }

        [JsonProperty(PropertyName = "caption")]
        public string Caption { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "mime_type")]
        public string MimeType { get; set; }

        [JsonProperty(PropertyName = "slug")]
        public string Slug { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "images")]
        public Images Images { get; set; }
    }
}