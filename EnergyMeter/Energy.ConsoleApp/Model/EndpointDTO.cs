using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy.ConsoleApp.Model
{
    internal class EndpointDTO
    {
        public string SerialNumber { get; set; }
        public string MeterModel { get; set; }
        public int MeterNumber { get; set; }
        public string FirmwareVersion { get; set; }
        public int State { get; set; }
        public string StateDesc { get; set; }
    }
}
