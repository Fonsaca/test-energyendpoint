using Autofac;
using Energy.ConsoleApp.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy.ConsoleApp.View
{
    internal abstract class BaseView
    {

        public static EndpointControl EndpointControl;

        public BaseView Caller { get; private set; }

        protected BaseView(BaseView caller) {
            this.Caller = caller;
        }

        internal abstract BaseView Render();


    }
}
