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
    internal class EditView : BaseView
    {
        public EditView(BaseView caller) : base(caller) {}

        internal override BaseView Render() {
            ConsoleExtensions.Clear();

            HeaderView();
            Title().Print();
            try {
                var serialNumber = GetInputSerialNumber(0);
                this.Searching().Print();
                if (EndpointExists(serialNumber)) {
                    Found().Print();
                    return Request(serialNumber, InputSwitchState());
                } else {
                    Notfound().Print();
                    "Press any key to get to the menu".Print();
                    ConsoleExtensions.GetLine();
                    return Caller;
                }

            } catch (Exception) {
                return Caller;
            }
        }

        private bool EndpointExists(string serialNumber) {
            var result = BaseView.EndpointControl.Find(serialNumber);
            return result.Success;
        }

        private BaseView Request(string serialNumber, int newState) {
            this.Updating().Print();
            return GetNextView(BaseView.EndpointControl.Edit(serialNumber, newState));
        }
        private BaseView GetNextView(ControlResult<object> result) {
            if (result.Success) {
                this.Success().Print();
                Thread.Sleep(3000);
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
Edit Endpoint
";
        }
        private string GetInputSerialNumber(int count) {
            if (count > 3)
                throw new InvalidOperationException("Serial Number");
            "Serial Number: ".PrintInline();
            string serialNumber = ConsoleExtensions.GetLine();
            if (string.IsNullOrEmpty(serialNumber)) {
                $"INVALID. Try Again ({3 - count})".Print();
                return GetInputSerialNumber(count + 1);
            }
            return serialNumber;
        }
        private int InputSwitchState() {
            State().PrintInline();
            return this.GetInputState(0);
        }

        private string Updating() {
            return "\nUpdating...";
        }
        private string Searching() {
            return "\nSearching...";
        }
        private string Fail(string message) {
            return $@"
Fail: {message}
Try Again in 3 seconds
";
        }
        private string Success() {
            return @"Endpoint Edited";
        }
        private string Notfound() {
            return @"Endpoint not found";
        }
        private string Found() {
            return @"Endpoint exists!";
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


    }
}
