using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFCAuditChecker.Models
{
    public class Device
    {
        public string GUID { get; set; }
        public string MACaddress { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string locationGUID { get; set; }
        public string RSSI { get; set; }
        public string permanentlyPowered { get; set; }
        public string batteryLevel { get; set; }
        public string inAlarm { get; set; }
        public string SSID { get; set; }
        public string firmwareVersion { get; set; }
        public string archived { get; set; }
        public string sampleRate { get; set; }
        public string transmissionPeriod { get; set; }
        public string lastReadingsReceived { get; set; }
        public string timeZone { get; set; }
        public string timeZoneOffset { get; set; }
        public string currentReadings { get; set; }
        public string channels { get; set; }
        public string inFavourites { get; set; }
        public string connectionLost { get; set; }
        public string auditChecksEnabled { get; set; }

        /*public class CurrentReadings
        {
            public string DateTime { get; set; }
            public string Channels { get; set; }
        }*/
    }
}
