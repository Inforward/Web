using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bespoke.Web.Models.Blog
{
    public class CategoryViewModel
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Url
        {
            get
            {
                return string.Concat("/blog/category/", Slug, "/");
            }
        }
    }
}