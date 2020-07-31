﻿using CRM.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;


namespace CRM.NUnitTest
{
    public class BaseTestServerFixture : IDisposable
    {
        public TestServer TestServer { get; }
        
        public HttpClient Client { get; }

        public BaseTestServerFixture()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<TestStartup>();

            TestServer = new TestServer(builder);
            Client = TestServer.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            TestServer.Dispose();
        }
    }
}
