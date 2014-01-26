using System.Linq;
using System.Web.Mvc;
using Bespoke.Infrastructure.Extensions;
using Bespoke.Services.Contracts;
using Bespoke.Web.Models.Home;

namespace Bespoke.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBlogService _blogService;

        public HomeController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public ActionResult Index()
        {
            var response = _blogService.GetRecentPosts().Clone();
            var posts = response.Posts;

            if (posts.Count < 10 && posts.Count > 0)
            {
                var morePosts = posts.Clone();

                while (posts.Count < 10)
                {
                    posts.AddRange(morePosts);
                }
            }

            var viewModel = new HomeViewModel()
            {
                RecentPosts = posts.Take(10).Select(BlogController.ToPostViewModel).ToList()
            };

            return View(viewModel);
        }
    }
}
