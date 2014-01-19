using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bespoke.Models.Blog;
using Newtonsoft.Json;

namespace Bespoke.Services.Messages.Blog.Wordpress
{
    public class GetPostsByTag : BaseResponse
    {
        [JsonProperty(PropertyName = "tag")]
        public Tag Tag { get; set; }

        [JsonProperty(PropertyName = "posts")]
        public List<Post> Posts { get; set; }
    }
}
