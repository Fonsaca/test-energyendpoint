using Autofac;
using Energy.Business.Interfaces;
using Energy.Business.Models;
using Energy.Infrastructure.DI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Energy.Test
{
    [TestClass]
    public class EndpointUnitTests
    {
        private ILifetimeScope scope;

        ~EndpointUnitTests() {
            scope.Dispose();
        }

        [TestInitialize]
        public void Init() {
            scope = DIMainModule.GetContainer().BeginLifetimeScope();
        }

        [TestMethod]
        public void AssertOnCreate_SerialNumberIsUnique() {
            IEndpointCRUDService service = scope.Resolve<IEndpointCRUDService>();
            var endpoint = new Endpoint(MeterModel.NSX1P2W, 1, "v1.0.0.0", "MOCK123");
            service.Create(endpoint);

            Assert.ThrowsException<InvalidOperationException>(() => service.Create(endpoint));
        }

        [TestMethod]
        public void AssertOnCreate_EndpointIsCreated() {
            IEndpointCRUDService service = scope.Resolve<IEndpointCRUDService>();
            var endpoint = new Endpoint(MeterModel.NSX1P2W, 1, "v1.0.0.0", "MOCK123");
            service.Create(endpoint);
            Assert.IsNotNull(service.Find(endpoint.SerialNumber));
        }

        [TestMethod]
        public void AssertOnEdit_EndpointDoesNotExist() {
            IEndpointCRUDService service = scope.Resolve<IEndpointCRUDService>();
            Assert.ThrowsException<KeyNotFoundException>(() => service.Edit("MOCK123", EndpointState.Disconnected));
        }

        [TestMethod]
        public void AssertOnEdit_EndpointStateErrorOnInvalidState() {
            IEndpointCRUDService service = scope.Resolve<IEndpointCRUDService>();
            var endpoint = new Endpoint(MeterModel.NSX1P2W, 1, "v1.0.0.0", "MOCK123");
            service.Create(endpoint);
            Assert.ThrowsException<KeyNotFoundException>(() => service.Edit(endpoint.SerialNumber, (EndpointState)10));
        }

        [TestMethod]
        public void AssertOnEdit_EndpointSuccessEdited() {
            IEndpointCRUDService service = scope.Resolve<IEndpointCRUDService>();
            var endpoint = new Endpoint(MeterModel.NSX1P2W, 1, "v1.0.0.0", "MOCK123");
            service.Create(endpoint);
            service.Edit(endpoint.SerialNumber, EndpointState.Armed);
            var editedEndpoint = service.Find(endpoint.SerialNumber);
            Assert.IsNotNull(editedEndpoint);
            Assert.IsTrue(editedEndpoint.SwitchState == endpoint.SwitchState);
        }

        [TestMethod]
        public void AssertOnDelete_EndpointDoesNotExist() {
            IEndpointCRUDService service = scope.Resolve<IEndpointCRUDService>();
            Assert.ThrowsException<KeyNotFoundException>(() => service.Delete("MOCK123"));
        }

        [TestMethod]
        public void AssertOnDelete_EndpointDeleted() {
            IEndpointCRUDService service = scope.Resolve<IEndpointCRUDService>();
            var endpoint = new Endpoint(MeterModel.NSX1P2W, 1, "v1.0.0.0", "MOCK123");
            service.Create(endpoint);
            service.Delete(endpoint.SerialNumber);
            Assert.IsNull(service.Find(endpoint.SerialNumber));
        }
    }
}
