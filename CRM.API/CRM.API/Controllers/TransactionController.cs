using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionStore.API.Models.Input;
using Newtonsoft.Json;
using CRM.Data;

namespace CRM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private static readonly HttpClient _httpClient;
        private static readonly LeadRepository _repo;
        static TransactionController()
        {
            _httpClient = new HttpClient();
            _repo = new LeadRepository();
        }

        [HttpPost("transfer")] // прописать BadRequests
        public async Task<ActionResult<List<long>>> CreateTransferTransaction([FromBody] TransferInputModel transactionModel)
        {
            if (_repo.GetById(transactionModel.DestinationLeadId) is null) return BadRequest("The user is deleted");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(transactionModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:44388/transaction/transfer", jsonContent);
            string content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
    }
}
