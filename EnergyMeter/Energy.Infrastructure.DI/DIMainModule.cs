using Autofac;
using Energy.Business.Interfaces;
using Energy.Data;
using Energy.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy.Infrastructure.DI
{
    public static class DIMainModule
    {
        public static IContainer GetContainer() {
            var builder = new ContainerBuilder();

            builder.RegisterType<EndpointRepository>().As<IEndpointRepository>().InstancePerDependency();
            builder.RegisterType<EndpointCRUDService>().As<IEndpointCRUDService>().InstancePerDependency();

            return builder.Build();

        }
    }
}
