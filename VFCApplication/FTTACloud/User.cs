using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFCApplication.FTTACloud
{
    public class User
    {
        public string GUID { get; set; }
        public string name { get; set; }
        public string dateFormat { get; set; }
        public string timeFormat { get; set; }
        public string groupAlertFrequency { get; set; }
        public string permissions { get; set; }
        
        /*public class Permissions
        {
            public string accountAdministrator;
            public string administrator;
            public string manageDevices;
            public string viewData;
            public string exportData;
            public string dailySummaryOfEvents;
            public string closeEvents;
        }*/
    }
}
