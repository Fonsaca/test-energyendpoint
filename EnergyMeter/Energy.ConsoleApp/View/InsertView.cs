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
    internal class InsertView : BaseView
    {
        internal InsertView(BaseView caller) : base(caller) {
        }

        internal override BaseView Render() {
            ConsoleExtensions.Clear();

            HeaderView();
            Title().Print();
            return Request(QuestionCarrousel());
        }

        private EndpointDTO QuestionCarrousel() {
            var model = new EndpointDTO();
            try {
                model.SerialNumber = InputSerialNumber(0);
                model.MeterModel = InputMeterModel(0);
                model.MeterNumber = InputMeterNumber(0);
                model.FirmwareVersion = InputMeterFirmwareVersion(0);
                model.State = InputSwitchState();
                return model;
            } catch (InvalidOperationException) {
                return null;
            }
        }
        private BaseView Request(EndpointDTO endpointDTO) {
            if (endpointDTO == null) {
                return Caller;
            }
            this.Inserting().Print();
            return GetNextView(BaseView.EndpointControl.Create(endpointDTO));
        }
        private BaseView GetNextView(ControlResult<object> result) {
            if (result.Success) {
                this.CreationSuccess().Print();
                Thread.Sleep(1500);
                return Caller;
            } else {
                CreationFailed(result.Message).Print();
                Thread.Sleep(3000);
                return Caller;
            }
        }

        private void HeaderView() {
            new HeaderView(this).Render();
        }
        private string Title() {
            return @"
Insert Endpoint
";
        }
        private string Inserting() {
            return @"
Creating...
";
        }
        private string CreationSuccess() {
            return @"Endpoint Created";
        }
        private string CreationFailed(string message) {
            return $@"
Fail: {message}
Try Again in 3 seconds
";
        }

  
        private string InputSerialNumber(int count) {
            if (count > 2)
                throw new InvalidOperationException("Serial Number");
            "Serial Number: ".PrintInline();
            string serialNumber = ConsoleExtensions.GetLine();
            if (string.IsNullOrEmpty(serialNumber)) {
                $"INVALID. Try Again ({3-count})".Print();
                return InputSerialNumber(count+1);
            }
            return serialNumber;
        }
        private string InputMeterModel(int count) {
            Model().PrintInline();
            return ReplaceModelIdToString(GetInputModel(0));
        }
        private int InputMeterNumber(int count) {
            if (count > 2)
                throw new InvalidOperationException("Meter Number");
            "Meter Number: ".PrintInline();
            int? number = ConsoleExtensions.GetInt();
            if (!number.HasValue) {
                $"INVALID. Try Again ({3 - count})".Print();
                return InputMeterNumber(count+1);
            }
            return number.Value;
 
        }
        private string InputMeterFirmwareVersion(int count) {
            if (count > 2)
                throw new InvalidOperationException("Meter Firmware Version");
            "Meter Firmware Version: ".PrintInline();
            string version = ConsoleExtensions.GetLine();
            if (string.IsNullOrEmpty(version)) {
                $"INVALID. Try Again ({3 - count})".Print();
                return InputMeterFirmwareVersion(count+1);
            }
            return version;
        }
        private int InputSwitchState() {
            State().PrintInline();
            return this.GetInputState(0);
        }

        private string State() {
            return @"Switch State:
    0 - Disconnected
    1 - Connected
    2 - Armed:
        Op: ";
        }
        private int GetInputState(int count) {
            if (count > 3)
                throw new InvalidOperationException();
            int[] validStates = new int[3] { 0, 1, 2 };
            var state = ConsoleExtensions.GetInt();
            if (!state.HasValue || !validStates.Contains(state.Value)) {
                InvalidState(count).Print();
                Op().PrintInline();
                return GetInputState(count + 1);
            }
            return state.Value;
        }
        private string InvalidState(int count) {
            return $"        Invalid state. Try again({3 - count})";
        }
        private string Op() {
            return $"        Op: ";
        }

        private string Model() {
            return @"Meter Model:
    16 - NSX1P2W
    17 - NSX1P3W
    18 - NSX2P3W
    19 - NSX3P4W:
        Op: ";
        }
        private int GetInputModel(int count) {
            if (count > 3)
                throw new InvalidOperationException();
            int[] validModels = new int[4] { 16,17,18,19 };
            var state = ConsoleExtensions.GetInt();
            if (!state.HasValue || !validModels.Contains(state.Value)) {
                InvalidModel(count).Print();
                Op().PrintInline();
                return GetInputModel(count + 1);
            }
            return state.Value;
        }
        private string ReplaceModelIdToString(int id) {
            switch (id) {
                case 16: 
                return "NSX1P2W";
                case 17:
                return "NSX1P3W";
                case 18:
                return "NSX2P3W";
                case 19:
                return "NSX3P4W";
                default:
                return string.Empty;
            }
        }
        private string InvalidModel(int count) {
            return $"        Invalid Meter Model. Try again({3 - count})";
        }

    }
}
