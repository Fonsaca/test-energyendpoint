using Energy.ConsoleApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy.ConsoleApp.View
{
    internal class MenuView : BaseView
    {
        private const int INSERT = 1;
        private const int EDIT = 2;
        private const int DELETE = 3;
        private const int LIST = 4;
        private const int FIND = 5;
        private const int EXIT = 6;

        internal MenuView(BaseView caller) : base(caller) { }
        internal override BaseView Render() {
            ConsoleExtensions.Clear();

            HeaderView();
            Title().Print();
            Menu().Print();

            return GetView();
        }

        private BaseView GetView() {
            return GetViewOfOperation(ConsoleExtensions.GetInt());
        }

        private BaseView GetViewOfOperation(int? op) {
            switch (op) {
                case INSERT:
                return new InsertView(this);
                case EDIT:
                return new EditView(this);
                case DELETE:
                return new DeleteView(this);
                case LIST:
                return new ListView(this);
                case FIND:
                return new FindView(this);
                case EXIT:
                return new ExitView(this);
                default:
                this.InvalidOperation().Print();
                this.Menu().Print();
                return this.GetView();
            }
        }

        private string InvalidOperation() {
            return @"
OPERATION NOT RECOGNIZED
";
        }

        private void HeaderView() {
            new HeaderView(this).Render();
        }

        private string Title() {
            return @"
Welcome to Energy Endpoints CRUD
";
        }

        private string Menu() {
            return $@"
Pick an option by typing the number:

{INSERT} - Insert new Endpoint
{EDIT} - Edit Enpoint
{DELETE} - Delete Endpoint
{LIST} - List Endpoints
{FIND} - Find Endpoint
{EXIT} - Exit

Op: ";
        }
    }
}
