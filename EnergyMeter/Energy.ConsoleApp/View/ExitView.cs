using Energy.ConsoleApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Energy.ConsoleApp.View
{
    internal class ExitView : BaseView
    {
        private const string YES_PATTERN = "Y|y";

        internal ExitView(BaseView caller) : base(caller) { }

        internal override BaseView Render() {
            Confirmation().Print();
            return getViewOfOperation(ConsoleExtensions.GetLine());
        }

        private string Confirmation() {
            return @"
Confirm to exit: 
(Y)es
(N)o";
        }

        private BaseView getViewOfOperation(string op) {
            var regex = new Regex(YES_PATTERN);
            if (!regex.IsMatch(op)) {
                return this.Caller;
            }
            return null;
        }
    }
}
