using Energy.Business.Interfaces;
using Energy.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy.Service
{
    public class EndpointCRUDService : IEndpointCRUDService
    {
        private readonly IEndpointRepository repository;

        public EndpointCRUDService(IEndpointRepository _repository) {
            repository = _repository;
        }

        public void Create(Endpoint endpoint) {
            if (endpoint == null)
                throw new ArgumentNullException();
            if (Find(endpoint.SerialNumber) != null)
                throw new InvalidOperationException("Endpoint with same serial number already exists");
            repository.Add(endpoint);
        }

        public void Delete(string serialNumber) {
            if (string.IsNullOrEmpty(serialNumber))
                throw new ArgumentNullException();
            var endpoint = Find(serialNumber);
            if (endpoint == null)
                throw new KeyNotFoundException("Endpoint not found");
            repository.Remove(endpoint);
        }

        public void Edit(string serialNumber, EndpointState state) {
            if (string.IsNullOrEmpty(serialNumber))
                throw new ArgumentNullException();
            var endpoint = Find(serialNumber);
            if (endpoint == null)
                throw new KeyNotFoundException("Endpoint not found");
            endpoint.SetState(state);
            repository.Update(endpoint);
        }

        public Endpoint Find(string serialNumber) {
            if (string.IsNullOrEmpty(serialNumber))
                throw new ArgumentNullException();
            return repository.Find(serialNumber);
        }

        public IEnumerable<Endpoint> List() {
            return repository.List();
        }
    }
}
