using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bespoke.Web.Models.Discussions
{
    public class MootConfigModel
    {
        public double Timestamp { get; set; }
        public string Message { get; set; }
        public string Signature { get; set; }
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
    }
}