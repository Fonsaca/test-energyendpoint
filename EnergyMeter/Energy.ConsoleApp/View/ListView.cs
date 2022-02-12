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
    internal class ListView : BaseView
    {
        public ListView(BaseView caller) : base(caller) {}

        internal override BaseView Render() {
            ConsoleExtensions.Clear();

            HeaderView();
            Title().Print();
            return Request();
        }

        private BaseView Request() {
            this.Searching().Print();
            return GetNextView(BaseView.EndpointControl.List());
        }
        private BaseView GetNextView(ControlResult<EndpointDTO> result) {
            if (result.Success) {
                if (!result.Result.Any()) {
                    "No endpoint found.".Print();
                } else {
                    result.Result
                        .Select(Display)
                        .Aggregate((a, b) => string.Concat(a, b))
                        .Print();
                }
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
List Endpoints
";
        }
        private string Searching() {
            return "\nSearching...\n";
        }
        private string Display(EndpointDTO dto) {
            return $@"
Serial Number: {dto.SerialNumber}
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
