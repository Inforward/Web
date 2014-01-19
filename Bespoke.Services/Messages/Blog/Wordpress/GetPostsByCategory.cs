using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bespoke.Models.Blog;
using Newtonsoft.Json;

namespace Bespoke.Services.Messages.Blog.Wordpress
{
    public class GetPostsByCategory : BaseResponse
    {
        [JsonProperty(PropertyName = "category")]
        public Category Category { get; set; }

        [JsonProperty(PropertyName = "pages")]
        public int Pages { get; set; }

        [JsonProperty(PropertyName = "posts")]
        public List<Post> Posts { get; set; }
    }
}
