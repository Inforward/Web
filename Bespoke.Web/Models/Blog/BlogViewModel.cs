using System.Collections.Generic;

namespace Bespoke.Web.Models.Blog
{
    public class BlogViewModel : PagerModel
    {
        public BlogViewModel()
        {
            Posts = new List<PostViewModel>();
            Categories = new List<CategoryViewModel>();
            Tags = new List<TagViewModel>();
            Archives = new List<ArchiveViewModel>();
        }

        public string PageTitle { get; set; }
        public string Filter { get; set; }
        public List<PostViewModel> Posts { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public List<ArchiveViewModel> Archives { get; set; }
    }
}