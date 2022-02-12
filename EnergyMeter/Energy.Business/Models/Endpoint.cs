using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy.Business.Models
{
    public class Endpoint : Meter
    {
        private string _serialNumber;
        private EndpointState _state;

        public string SerialNumber {
            get {
                return _serialNumber;
            }
            private set {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Missing Serial Number");
                _serialNumber = value;
            }
        }
        public int SwitchState { get { return (int)_state; } }


        public Endpoint(MeterModel meterModel, int number, string firmwareVersion, string serialNumber) : base(meterModel, number, firmwareVersion) {
            SerialNumber = serialNumber;
            this.SetState(EndpointState.Disconnected);
        }
        public string GetStateName() {
            return _state.ToString();
        }
        public void SetState(EndpointState state) {
            if (!Enum.IsDefined(typeof(EndpointState), state))
                throw new KeyNotFoundException("Endpoint not recognized");
            this._state = state;
        }
    }

    public enum EndpointState
    {
        Disconnected = 0,
        Connected = 1,
        Armed = 2
    }
}
