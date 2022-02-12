using Energy.ConsoleApp.Model;
using Energy.ConsoleApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Energy.ConsoleApp.View
{
    internal class FindView : BaseView
    {
        public FindView(BaseView caller) : base(caller) {}

        internal override BaseView Render() {
            ConsoleExtensions.Clear();

            HeaderView();
            Title().Print();
            return Request(GetInputSerialNumber());
        }

        private BaseView Request(string endpoint) {
            if (string.IsNullOrEmpty(endpoint)) {
                InvalidSerialNumber().Print();
                Thread.Sleep(1000);
                return Caller;
            }
            this.Searching().Print();
            return GetNextView(BaseView.EndpointControl.Find(endpoint));
        }
        private BaseView GetNextView(ControlResult<EndpointDTO> result) {
            if (result.Success) {
                this.Display(result.Result.First()).Print();
                "Press any key to get back to menu".Print();
                ConsoleExtensions.GetLine();
                return Caller;
            } else {
                Fail(result.Message).Print();
                Thread.Sleep(3000);
                return Caller;
            }
        }


        private void HeaderView() {
            new HeaderView(this).Render();
        }
        private string Title() {
            return @"
Find Endpoint
";
        }
        private string GetInputSerialNumber() {
            "\nInform the serial number: ".PrintInline();
            return ConsoleExtensions.GetLine();
        }
        private string InvalidSerialNumber() {
            return "\nInvalid serial number";
        }
        private string Searching() {
            return "\nSearching...\n";
        }
        private string Display(EndpointDTO dto) {
            return $@"Serial Number: {dto.SerialNumber}
Meter Model: {dto.MeterModel}
Meter Number: {dto.MeterNumber}
Firmware Version: {dto.FirmwareVersion}
Switch State: {dto.State} - {dto.StateDesc}
";
        }
        private string Fail(string message) {
            return $@"
Fail: {message}
Try Again in 3 seconds
";
        }

    }
}
