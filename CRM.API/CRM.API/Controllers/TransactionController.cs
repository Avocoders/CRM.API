using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private static readonly HttpClient httpClient;

        static TransactionController()
        {
            httpClient = new HttpClient();
        }


    }
}
