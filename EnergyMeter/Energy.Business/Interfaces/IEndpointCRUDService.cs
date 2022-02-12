using Energy.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy.Business.Interfaces
{
    public interface IEndpointCRUDService
    {
        void Create(Endpoint endpoint);
        void Edit(string serialNumber, EndpointState state);
        void Delete(string serialNumber);

        IEnumerable<Endpoint> List();
        Endpoint Find(string serialNumber);

    }
}
