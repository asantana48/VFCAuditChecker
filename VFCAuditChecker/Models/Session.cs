using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFCAuditChecker.Models
{
    public class Session
    {
        public string GUID { get; set; }
        public string noOfReadings { get; set; }
        public string firstReading { get; set; }
        public string lastReading { get; set; }
        public string MACAddress { get; set; }
        public string deviceName { get; set; }
        public string timeZone { get; set; }
        public string timeZoneOffset { get; set; }
        public string sampleRate { get; set; }
        public string channels { get; set; }
    }
}
