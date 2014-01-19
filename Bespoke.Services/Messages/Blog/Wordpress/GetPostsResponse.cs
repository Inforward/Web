using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bespoke.Models.Blog;
using Newtonsoft.Json;

namespace Bespoke.Services.Messages.Blog.Wordpress
{
    internal class GetPostsResponse : BaseResponse
    {
        [JsonProperty(PropertyName = "posts")]
        public List<Post> Posts { get; set; }
    }
}
