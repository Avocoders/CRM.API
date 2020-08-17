﻿using Autofac;
using CRM.Core;
using CRM.Data;

namespace CRM.API.Configuration
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LeadRepository>().As<ILeadRepository>();
            builder.RegisterType<DatabaseOptions>().As<IDatabaseOptions>();
            builder.RegisterType<UrlOptions>().As<IUrlOptions>();
        }
    }
}
