using Autofac;
using Energy.Business.Interfaces;
using Energy.ConsoleApp.Control;
using Energy.ConsoleApp.View;
using Energy.Infrastructure.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy.ConsoleApp
{
    class Program
    {
        static void Main(string[] args) {

            var container = DIMainModule.GetContainer();
            using (var scope = container.BeginLifetimeScope()) {
                SetControl(scope);
                Run();
            }

        }

        static void Run() {
            BaseView view = new MenuView(null);
            while (view != null) { view = view.Render(); }
        }

        static void SetControl(ILifetimeScope scope) {
            BaseView.EndpointControl = new EndpointControl(scope.Resolve<IEndpointCRUDService>());
        }
    }
}
