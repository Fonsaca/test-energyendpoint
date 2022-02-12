using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy.ConsoleApp.Model
{
    internal class ControlResult<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<T> Result { get; set; } = new List<T>();
    }

}
