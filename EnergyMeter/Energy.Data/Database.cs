using Energy.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy.Data
{
    internal class Database
    {
        internal ICollection<Endpoint> Endpoints { get; set; } = new List<Endpoint>();
    }
}
