using System.Collections.Generic;
using Bespoke.Web.Models.Blog;

namespace Bespoke.Web.Models
{
    public class HomeViewModel
    {
        public List<PostViewModel> RecentPosts { get; set; }
    }
}