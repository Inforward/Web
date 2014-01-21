using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bespoke.Web.Models
{
    public class PagerModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string NextPageUrl { get; set; }
        public string PreviousPageUrl { get; set; }
    }
}