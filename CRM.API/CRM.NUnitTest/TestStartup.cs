using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data;
using CRM.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Autofac;
using CRM.API.Configuration;

namespace CRM.NUnitTest
{
    public class TestStartup : Startup
    {
        public TestStartup(IWebHostEnvironment testenv) : base(testenv)
        {

        }
    }


}


