using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SqlServer.Server;

namespace Bespoke.Web.Models.Blog
{
    public class TagViewModel
    {
        public string Text { get; set; }
        public string Slug { get; set; }
        public string TagCloudFontSize { get; set; }
        public int OccurenceCount { get; set; }
        public string Url
        {
            get
            {
                return string.Concat("/blog/tag/", Slug, "/");
            }
        }
        public string Title
        {
            get
            {
                return string.Format("{0} article{1}", OccurenceCount, OccurenceCount == 1 ? string.Empty : "s");
            }
                
        }
    }
}