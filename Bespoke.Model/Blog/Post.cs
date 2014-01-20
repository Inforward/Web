using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bespoke.Models.Blog
{
    [Serializable]
    public class Post
    {
        public Post()
        {
            Author = new Author();
            Categories = new List<Category>();
            Tags = new List<Tag>();
        }

        [JsonProperty(PropertyName = "id")]
        public int PostId { get; set; }

        [JsonProperty(PropertyName = "comment_count")]
        public int CommentCount { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "excerpt")]
        public string Excerpt { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "slug")]
        public string Slug { get; set; }

        [JsonProperty(PropertyName = "date")]
        public string CreateDateString { get; set; }

        [JsonProperty(PropertyName = "modified")]
        public string ModifyDateString { get; set; }

        [JsonProperty(PropertyName = "thumbnail")]
        public string ThumbnailImageUrl { get; set; }

        public DateTime CreateDate
        {
            get
            {
                DateTime d;

                return DateTime.TryParse(CreateDateString, out d) ? d : d;
            }
        }

        public DateTime ModifyDate
        {
            get
            {
                DateTime d;

                return DateTime.TryParse(ModifyDateString, out d) ? d : d;
            }
        }

        [JsonProperty(PropertyName = "author")]
        public Author Author { get; set; }

        [JsonProperty(PropertyName = "thumbnail_images")]
        public Images ThumbnailImages { get; set; }

        [JsonProperty(PropertyName = "categories")]
        public List<Category> Categories { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty(PropertyName = "attachments")]
        public List<Attachment> Attachments { get; set; }
    }
}