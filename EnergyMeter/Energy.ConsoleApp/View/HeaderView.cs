using Energy.ConsoleApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy.ConsoleApp.View
{
    internal class HeaderView : BaseView
    {
        internal HeaderView(BaseView caller): base(caller) { }

        internal override BaseView Render() {
            Header().Print();
            return null;
        }


        private string Header() {
            return @"
########################################################
## Energy Endpoints                                   ##
## Author: Gabriel Fonsaca                            ##
########################################################
";
        }




    }
}
