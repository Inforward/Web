using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bespoke.Models.Blog;
using Newtonsoft.Json;

namespace Bespoke.Services.Messages.Blog.Wordpress
{
    public class GetCategoriesResponse : BaseResponse
    {
        [JsonProperty(PropertyName = "categories")]
        public List<Category> Categories { get; set; }
    }
}
