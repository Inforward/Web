using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Bespoke.Infrastructure.Extensions;
using Bespoke.Models.Blog;
using Bespoke.Services.Contracts;
using Bespoke.Web.Helpers;
using Bespoke.Web.Models;
using Bespoke.Web.Models.Blog;
using Bespoke.Web.Helpers;

namespace Bespoke.Web.Controllers
{
    [RoutePrefix("blog")]
    public class BlogController : Controller
    {
        #region Private Members

        private readonly IBlogService _blogService;

        #endregion

        #region Controller

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        #endregion

        #region Actions

        public ActionResult Index()
        {
            var viewModel = new BlogViewModel()
                {
                    Posts = _blogService.GetRecentPosts().Select(ToPostViewModel).ToList()
                };

            return View(viewModel);
        }

        [Route("author/{slug}")]
        [Route("author/{slug}/page/{id?}")]
        public ActionResult Author(string slug, int? id)
        {
            var viewModel = new BlogViewModel()
            {
                Posts = _blogService.GetPostsByAuthor(slug).Select(ToPostViewModel).ToList()
            };

            return View("Posts", viewModel);
        }

        [Route("{year:int}/{month:int}")]
        public ActionResult Archive(int year, int month)
        {
            var posts = _blogService.GetPostsByArchive(year, month).Select(ToPostViewModel).ToList();

            var viewModel = new BlogViewModel()
                {
                    Posts = posts,
                    Filter = string.Format("MONTHLY ARCHIVES: {0:MMMM yyyy}", new DateTime(year, month, 1))
                };

            return View("Posts", viewModel);
        }

        [Route("{year:int}/{month:int}/{day:int}/{title}")]
        public ActionResult Detail(int year, int month, int day, string title)
        {
            var permalink = string.Format("/{0}/{1}/{2}/{3}", year, month, day, title);
            var post = _blogService.GetPost(permalink);

            return View(ToPostViewModel(post));
        }

        [Route("category/{slug}")]
        [Route("category/{slug}/page/{id?}")]
        public ActionResult Category(string slug, int? id)
        {
            var posts = _blogService.GetPostsByCategory(slug).Select(ToPostViewModel).ToList();

            var viewModel = new BlogViewModel()
                {
                    Posts = posts,
                    Filter = string.Format("CATEGORY: {0}", slug), // TODO: Category Title
                    PageTitle = slug // TODO: Category Title
                };

            return View("Posts", viewModel);
        }

        [Route("tag/{slug}")]
        [Route("tag/{slug}/page/{id?}")]
        public ActionResult Tag(string slug, int? id)
        {
            var posts = _blogService.GetPostsByTag(slug).Select(ToPostViewModel).ToList();

            var viewModel = new BlogViewModel()
                {
                    Posts = posts,
                    Filter = string.Format("TAG: {0}", slug), // TODO: Tag Title
                    PageTitle = slug // TODO: Tag Title
                };

            return View("Posts", viewModel);
        }

        [Route("search")]
        public ActionResult Search(string s)
        {
            var viewModel = new BlogViewModel()
                {
                    Filter = string.Format("Search Results for: {0}", s)
                };

            var posts = _blogService.SearchPosts(s).Select(ToPostViewModel).ToList();

            if (!string.IsNullOrWhiteSpace(s))
            {
                viewModel.Posts = posts;
            }

            return View("Posts", viewModel);
        }

        public PartialViewResult Sidebar()
        {
            var viewModel = new BlogViewModel()
                {
                    Posts = _blogService.GetRecentPosts().Select(ToPostViewModel).ToList(),
                    Archives = _blogService.GetPostArchiveTree().Select(ToArchiveViewModel).ToList(),
                    Tags = GetTagCloud()
                };

            return PartialView(viewModel);
        }

        public PartialViewResult Categories()
        {
            var categories = _blogService.GetCategories().Select(ToCategoryViewModel).ToList();

            return PartialView(categories);
        }

        #endregion

        #region Mapping Methods

        public static PostViewModel ToPostViewModel(Post post)
        {
            var viewModel = new PostViewModel
                {
                    Post = post, 
                    Tags = post.Tags.Select(ToTagViewModel).ToList()
                };

            return viewModel;

        }

        public static CategoryViewModel ToCategoryViewModel(Category category)
        {
            return new CategoryViewModel()
                {
                    Title = category.Title,
                    Slug = category.Slug
                };
        }

        public static TagViewModel ToTagViewModel(Tag tag)
        {
            return new TagViewModel()
                {
                    Text = tag.Title,
                    Slug = tag.Slug,
                    OccurenceCount = tag.PostCount
                };
        }

        public static ArchiveViewModel ToArchiveViewModel(Archive archive)
        {
            return new ArchiveViewModel()
                {
                    Text = string.Format("{0:MMMM yyyy}", archive.ArchiveDate),
                    Url = string.Format("/blog/{0:yyyy}/{0:MM}", archive.ArchiveDate)
                };
        }

        #endregion

        #region Private Methods

        private List<TagViewModel> GetTagCloud()
        {
            var list = new List<TagViewModel>();
            const int maxFontSize = 22;
            const int minFontSize = 8;

            var tags = _blogService.GetTags().OrderBy(t => t.Title).ToList();

            if (!tags.Any())
                return list;

            var max = tags.Max(t => t.PostCount);

            foreach (var tag in tags)
            {
                var percent = (tag.PostCount/(decimal)max);
                var fontSize = maxFontSize*percent;

                if (fontSize < minFontSize)
                    fontSize = minFontSize;
                
                var viewModel = ToTagViewModel(tag);
                viewModel.TagCloudFontSize = string.Format("{0}pt", fontSize);

                list.Add(viewModel);
            }

            return list;
        }

        #endregion
    }
}