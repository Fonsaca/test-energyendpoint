using Energy.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy.Business.Interfaces
{
    public interface IEndpointRepository
    {
        void Add(Endpoint endpoint);
        void Update(Endpoint endpoint);
        void Remove(Endpoint endpoint);
        Endpoint Find(string serialNumber);
        List<Endpoint> List();
    }
}
