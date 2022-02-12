using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy.Business.Models
{
    public class Meter
    {
        private int _number;
        private string _firmwareVersion;
        private MeterModel _meterModel;
        

        public int ModelId { get { return (int)_meterModel; } }
        public int Number {
            get {
                return _number;
            }
            private set {
                if (value <= 0)
                    throw new ArgumentException("Number should be greater than 0");
                this._number = value;
            }
        }
        public string FirmwareVersion { 
            get {
                return _firmwareVersion;
            }
            private set {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Missing firmware version");
                _firmwareVersion = value;
            }
        }

        public Meter(MeterModel meterModel, int number, string firmwareVersion) {
            SetMeterModel(meterModel);
            Number = number;
            FirmwareVersion = firmwareVersion;
        }

        public string GetModelName() {
            return _meterModel.ToString();
        }
        private void SetMeterModel(MeterModel meterModel) {
            if (!Enum.IsDefined(typeof(MeterModel), meterModel))
                throw new KeyNotFoundException("Meter model not recognized");
            this._meterModel = meterModel;
        }
    }
}
