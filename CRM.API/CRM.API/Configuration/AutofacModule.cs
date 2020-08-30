using Autofac;
using CRM.API.Controllers;
using CRM.Core;
using CRM.Data;

namespace CRM.API.Configuration
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LeadRepository>().As<ILeadRepository>();
            builder.RegisterType<OperationRepository> ().As<IOperationRepository>();
            builder.RegisterType<LeadSearchParameters>().SingleInstance();
            builder.RegisterType<DatabaseOptions>().As<IDatabaseOptions>();
            builder.RegisterType<UrlOptions>().As<IUrlOptions>();
            builder.RegisterType<Validator>().SingleInstance();
            builder.RegisterType<ResponseWrapper>().SingleInstance();
            builder.RegisterType<TransactionController>().SingleInstance();

        }
    }
}
