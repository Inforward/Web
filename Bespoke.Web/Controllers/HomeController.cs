using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bespoke.Services.Contracts;
using Bespoke.Web.Models;
using Bespoke.Web.Models.Blog;

namespace Bespoke.Web.Controllers
{
    public class HomeController : Controller
    {
         private readonly IBlogService _blogService;

         public HomeController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public ActionResult Index()
        {
            var viewModel = new HomeViewModel()
            {
                RecentPosts = _blogService.GetRecentPosts().Take(3).Select(BlogController.ToPostViewModel).ToList()
            };

            return View(viewModel);
        }
    }
}
