using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data;
using CRM.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CRM.NUnitTest
{
   public class TestStartup : Startup
    {
        public TestStartup(IWebHostEnvironment testenv) : base(testenv)
        {
        }

        public override void ConfigureDependencies(IServiceCollection services)
        {
            base.ConfigureDependencies(services);
        }

    }
}
