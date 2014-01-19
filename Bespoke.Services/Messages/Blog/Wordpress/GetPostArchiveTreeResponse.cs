using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bespoke.Models.Blog;
using Newtonsoft.Json;

namespace Bespoke.Services.Messages.Blog.Wordpress
{
    public class GetPostArchiveTreeResponse : BaseResponse
    {
        [JsonProperty(PropertyName = "permalinks")]
        public List<string> Permalinks { get; set; }

        [JsonProperty(PropertyName = "tree")]
        public Dictionary<int, Dictionary<int, int>> Tree { get; set; }
    }
}
