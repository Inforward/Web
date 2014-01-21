using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bespoke.Models.Blog;
using Bespoke.Services.Contracts;
using Bespoke.Services.Messages.Blog;
using Bespoke.Web.Models.Blog;

namespace Bespoke.Web.Controllers
{
    [RoutePrefix("blog")]
    public class BlogController : BaseController
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

        [Route]
        [Route("page/{id?}")]
        public ActionResult Index(int? id)
        {
            var response = _blogService.GetRecentPosts(id ?? 1);

            var viewModel = GetBlogViewModel(response, Url.Action("Index"));

            return View(viewModel);
        }

        [Route("author/{slug}")]
        [Route("author/{slug}/page/{id?}")]
        public ActionResult Author(string slug, int? id)
        {
            var response = _blogService.GetPostsByAuthor(slug, id ?? 1);

            var viewModel = GetBlogViewModel(response, Url.Action("Author", new { slug }));

            viewModel.Filter = "ALL POSTS BY: " + response.Author.Name;

            return View("Posts", viewModel);
        }

        [Route("{year:int}/{month:int}")]
        [Route("{year:int}/{month:int}/page/{id?}")]
        public ActionResult Archive(int year, int month, int? id)
        {
            var response = _blogService.GetPostsByArchive(year, month, id ?? 1);

            var viewModel = GetBlogViewModel(response, Url.Action("Archive", new { year, month }));

            viewModel.Filter = string.Format("MONTHLY ARCHIVES: {0:MMMM yyyy}", new DateTime(year, month, 1));

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
            var response = _blogService.GetPostsByCategory(slug, id ?? 1);

            var viewModel = GetBlogViewModel(response, Url.Action("Category", new { slug }));

            viewModel.Filter = string.Format("CATEGORY: {0}", response.Category.Title);

            return View("Posts", viewModel);
        }

        [Route("tag/{slug}")]
        [Route("tag/{slug}/page/{id?}")]
        public ActionResult Tag(string slug, int? id)
        {
            var response = _blogService.GetPostsByTag(slug, id ?? 1);

            var viewModel = GetBlogViewModel(response, Url.Action("Tag", new { slug }));

            viewModel.Filter = string.Format("TAG: {0}", response.Tag.Title);

            return View("Posts", viewModel);
        }

        [Route("search")]
        [Route("search/page/{id?}")]
        public ActionResult Search(string s, int? id = 1)
        {
            var viewModel = new BlogViewModel();

            if (!string.IsNullOrWhiteSpace(s))
            {
                var response = _blogService.SearchPosts(s, id ?? 1);

                viewModel = GetBlogViewModel(response, Url.Action("Search", new { s }));
            }

            viewModel.Filter = string.Format("Search Results for: {0}", s);

            return View("Posts", viewModel);
        }

        public PartialViewResult Sidebar()
        {
            var response = _blogService.GetRecentPosts();

            var viewModel = new BlogViewModel()
                {
                    Posts = response.Posts.Take(5).Select(ToPostViewModel).ToList(),
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
            return new PostViewModel
                {
                    Post = post, 
                    Tags = post.Tags.Select(ToTagViewModel).ToList()
                };
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

        private BlogViewModel GetBlogViewModel(GetPostsResponse response, string baseUrl)
        {
            var viewModel = new BlogViewModel()
            {
                Posts = response.Posts.Select(ToPostViewModel).ToList(),
                CurrentPage = response.Page,
                TotalPages = response.Pages
            };

            if (viewModel.CurrentPage > 1)
            {
                viewModel.PreviousPageUrl = string.Format("{0}/page/{1}", baseUrl, viewModel.CurrentPage - 1);
            }

            if (viewModel.CurrentPage < viewModel.TotalPages)
            {
                viewModel.NextPageUrl = string.Format("{0}/page/{1}", baseUrl, viewModel.CurrentPage + 1);
            }

            if (!string.IsNullOrEmpty(viewModel.PreviousPageUrl))
                AddLinkTag("prev", viewModel.PreviousPageUrl);

            if (!string.IsNullOrEmpty(viewModel.NextPageUrl))
                AddLinkTag("next", viewModel.NextPageUrl);

            return viewModel;
        }

        #endregion
    }
}