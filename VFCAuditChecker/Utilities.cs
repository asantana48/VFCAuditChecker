using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFCAuditChecker
{
    public static class Utilities
    {
        public static DateTime FromUnixEpoch(this DateTime dt, long timestamp)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0);
            epoch = epoch.AddMilliseconds(timestamp);
            return epoch;
        }
        public static DateTime FromCloudAPI(this DateTime dt, string date)
        {
            int start = date.IndexOf("(") + 1;
            int end = date.IndexOf("+");
            string subdate = date.Substring(start, end - start);
            long timestamp = Convert.ToInt64(subdate);
            return new DateTime().FromUnixEpoch(timestamp);
        }
    }
}
