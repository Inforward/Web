using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bespoke.Services.Messages.Blog
{
    [Serializable]
    public class GetPostArchiveTreeResponse : BaseResponse
    {
        [JsonProperty(PropertyName = "permalinks")]
        public List<string> Permalinks { get; set; }

        [JsonProperty(PropertyName = "tree")]
        public Dictionary<int, Dictionary<int, int>> Tree { get; set; }
    }
}