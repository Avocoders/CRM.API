using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using CRM.Core;
using CRM.Data;

namespace CRM.API.Configuration
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            CrmAppContext.ContainerBuilder.RegisterType<LeadRepository>().As<ILeadRepository>();
        }
    }
}
