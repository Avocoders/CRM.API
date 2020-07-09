using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionStore.API.Models.Input;
using Newtonsoft.Json;

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

        [HttpPost("transfer")] // прописать BadRequests
        public async Task<ActionResult<List<long>>> CreateTransferTransaction([FromBody] TransferInputModel transactionModel)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(transactionModel), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:44388/transaction/transfer", jsonContent);
            string content = await response.Content.ReadAsStringAsync();
            List<long> list = JsonConvert.DeserializeObject<List<long>>(content);
            return Ok(list);
        }
    }
}
