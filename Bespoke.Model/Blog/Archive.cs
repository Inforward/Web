using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bespoke.Models.Blog
{
    [Serializable]
    public class Archive
    {
        public DateTime ArchiveDate { get; set; }
        public int PostCount { get; set; }
    }
}
