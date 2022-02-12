using Energy.Business.Interfaces;
using Energy.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy.Data
{
    public class EndpointRepository : IEndpointRepository
    {

        private readonly Database Db;

        public EndpointRepository() {
            Db = new Database();
        }

        public void Add(Endpoint endpoint) {
            Db.Endpoints.Add(endpoint);
        }

        public Endpoint Find(string serialNumber) {
            return Db.Endpoints.FirstOrDefault(x => x.SerialNumber == serialNumber);
        }

        public List<Endpoint> List() {
            var query =     from ep in Db.Endpoints
                            select ep;
            return query.ToList();
        }

        public void Remove(Endpoint endpoint) {
            Db.Endpoints.Remove(endpoint);
        }

        public void Update(Endpoint endpoint) {
            Remove(Find(endpoint.SerialNumber));
            Add(endpoint);
        }
    }
}
