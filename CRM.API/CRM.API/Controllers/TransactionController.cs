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
using CRM.API.Models.Output;

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

        [HttpPost("transfer")]
        public async Task<ActionResult<List<long>>> CreateTransferTransaction([FromBody] TransferInputModel transactionModel)
        {
            if (_repo.GetById(transactionModel.DestinationLeadId) is null) return BadRequest("The user is deleted");

            var jsonContent = new StringContent(JsonConvert.SerializeObject(transactionModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:44388/transaction/transfer", jsonContent);
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        [HttpPost("withdraw")]
        public async Task<ActionResult<long>> CreateWithdrawTransaction([FromBody] TransactionInputModel transactionModel)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(transactionModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:44388/transaction/withdraw", jsonContent);
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        [HttpPost("deposit")]
        public async Task<ActionResult<long>> CreateDepositTransaction([FromBody] TransactionInputModel transactionModel)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(transactionModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:44388/transaction/deposit", jsonContent);
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        [HttpGet("by-lead-id/{leadId}")]
        public async Task<ActionResult<List<TransferOutputModel>>> GetTransactionsByLeadId(long leadId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44388/transaction/by-lead-id/{leadId}");
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionOutputModel>> GetTransactionById(long id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44388/transaction/{id}");
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        [HttpGet("{leadId}/balance/{currencyId}")]
        public async Task<ActionResult<decimal>> GetBalanceByLeadIdInCurrency(long leadId, byte currencyId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44388/transaction/{leadId}/balance/{currencyId}");
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        /*
        [HttpGet("by-lead-id/{leadId}/range-date")]
        public ActionResult<List<TransferOutputModel>> GetRangeDateTransactionByLeadId([FromBody] RangeDateInputModel rangeModel)
        {
            return _mapper.ConvertTransferTransactionDtosToTransferOutputModels(_repo.GetRangeDateByLeadId(_mapper.ConvertRangeDateInputModelToRangeDateDto(rangeModel)));
        }
        */

    }
}
