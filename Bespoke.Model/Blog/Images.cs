using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bespoke.Models.Blog
{
    [Serializable]
    public class Images
    {
        [JsonProperty(PropertyName = "full")]
        public Image Full { get; set; }

        [JsonProperty(PropertyName = "thumbnail")]
        public Image Thumbnail { get; set; }

        [JsonProperty(PropertyName = "medium")]
        public Image Medium { get; set; }

        [JsonProperty(PropertyName = "large")]
        public Image Large { get; set; }

        [JsonProperty(PropertyName = "post-thumbnail")]
        public Image PostThumbnail { get; set; }
        
    }
}
