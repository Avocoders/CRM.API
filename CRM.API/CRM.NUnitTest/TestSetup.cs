using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CRM.NUnitTest
{
    public abstract class TestSetup
    {
        public HttpClient httpClient;
        
        public string connectionString = "Data Source=31.31.196.234;Initial Catalog = u1093787_CRM_Test; User Id = u1093787_User; Password = Etcr0?38";
       
       
    }

}
