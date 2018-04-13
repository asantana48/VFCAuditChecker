using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFCAuditChecker.Models
{
    public class Reading
    {
        public String sessionGUID { get; set; }
        public String datetime {get; set;}
        public String readingType { get; set; }
        public String channels { get; set; }
    }
}
