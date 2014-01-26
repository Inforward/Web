using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Bespoke.Web.Models
{
    public class JsonResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
    }
}