using Energy.Business.Interfaces;
using Energy.Business.Models;
using Energy.ConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy.ConsoleApp.Control
{
    internal class EndpointControl
    {
        private readonly IEndpointCRUDService Service;
        public EndpointControl(IEndpointCRUDService service) {
            Service = service;
        }

        public ControlResult<object> Create(EndpointDTO endpointDTO) {
            try {
                Enum.TryParse(endpointDTO.MeterModel, out MeterModel model);
                var endpoint = new Endpoint(model
                    , endpointDTO.MeterNumber
                    , endpointDTO.FirmwareVersion
                    , endpointDTO.SerialNumber);

                endpoint.SetState((EndpointState)endpointDTO.State);
                Service.Create(endpoint);
                return new ControlResult<object>() { Success = true, Message = string.Empty };
            } catch (Exception ex) {
                return new ControlResult<object>() { Success = false, Message = ex.Message };
            }
        }
        public ControlResult<EndpointDTO> Find(string serialNumber) {
            try {
                var endpoint = Service.Find(serialNumber);
                if(endpoint == null)
                    return new ControlResult<EndpointDTO>() { Success = false, Message = "Endpoint not found" };

                var result = new List<EndpointDTO>() {
                    new EndpointDTO() {
                        FirmwareVersion = endpoint.FirmwareVersion,
                        MeterModel = endpoint.GetModelName(),
                        MeterNumber = endpoint.Number,
                        SerialNumber = endpoint.SerialNumber,
                        State = endpoint.SwitchState,
                        StateDesc = endpoint.GetStateName()
                    }
                };
                return new ControlResult<EndpointDTO>() { Success = true, Message = string.Empty, Result = result };
            } catch (Exception ex) {
                return new ControlResult<EndpointDTO>() { Success = false, Message = ex.Message };
            }
        }
        public ControlResult<EndpointDTO> List() {
            try {
                var endpoints = Service.List();
                
                var result = endpoints.Select(endpoint =>
                    new EndpointDTO() {
                        FirmwareVersion = endpoint.FirmwareVersion,
                        MeterModel = endpoint.GetModelName(),
                        MeterNumber = endpoint.Number,
                        SerialNumber = endpoint.SerialNumber,
                        State = endpoint.SwitchState,
                        StateDesc = endpoint.GetStateName()
                    }
                );
                return new ControlResult<EndpointDTO>() { Success = true, Message = string.Empty, Result = result };
            } catch (Exception ex) {
                return new ControlResult<EndpointDTO>() { Success = false, Message = ex.Message };
            }
        }
        public ControlResult<object> Edit(string serialNumber, int state) {
            try {
                Service.Edit(serialNumber, (EndpointState)state);
                return new ControlResult<object>() { Success = true, Message = string.Empty };
            } catch (Exception ex) {
                return new ControlResult<object>() { Success = false, Message = ex.Message };
            }
        }
        public ControlResult<object> Delete(string serialNumber) {
            try {
                Service.Delete(serialNumber);
                return new ControlResult<object>() { Success = true, Message = string.Empty };
            } catch (Exception ex) {
                return new ControlResult<object>() { Success = false, Message = ex.Message };
            }
        }

    }
}
