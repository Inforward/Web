using System;
using System.Collections.Generic;
using Bespoke.Models.Blog;
using Newtonsoft.Json;

namespace Bespoke.Services.Messages.Blog.Wordpress
{
    [Serializable]
    public class GetCategoriesResponse : BaseResponse
    {
        [JsonProperty(PropertyName = "categories")]
        public List<Category> Categories { get; set; }
    }
}