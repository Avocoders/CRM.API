using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionStore.API.Models.Input;
using Newtonsoft.Json;
using CRM.Data;
using CRM.API.Models.Output;
using Microsoft.AspNetCore.Http;

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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("transfer")]
        public async Task<ActionResult<List<long>>> CreateTransferTransaction([FromBody] TransferInputModel transactionModel)
        {
            if (_repo.GetById(transactionModel.LeadId).Data is null) return BadRequest("The user is not found");
            if (_repo.GetById(transactionModel.DestinationLeadId).Data is null) return BadRequest("The user is not found");
            if (transactionModel.Amount <= 0) return BadRequest("The amount is missing");
            if (transactionModel.CurrencyId <= 0) return BadRequest("The currency is missing");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(transactionModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:44388/transaction/transfer", jsonContent);
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("withdraw")]
        public async Task<ActionResult<long>> CreateWithdrawTransaction([FromBody] TransactionInputModel transactionModel)
        {
            if (_repo.GetById(transactionModel.LeadId) is null) return BadRequest("The user is not found");
            if (transactionModel.Amount <= 0) return BadRequest("The amount is missing");
            if (transactionModel.CurrencyId <= 0) return BadRequest("The currency is missing");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(transactionModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:44388/transaction/withdraw", jsonContent);
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("deposit")]
        public async Task<ActionResult<long>> CreateDepositTransaction([FromBody] TransactionInputModel transactionModel)
        {
            if (_repo.GetById(transactionModel.LeadId) is null) return BadRequest("The user is not found");
            if (transactionModel.Amount <= 0) return BadRequest("The amount is missing");
            if (transactionModel.CurrencyId <= 0) return BadRequest("The currency is missing");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(transactionModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:44388/transaction/deposit", jsonContent);
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("by-lead-id/{leadId}")]
        public async Task<ActionResult<List<TransferOutputModel>>> GetTransactionsByLeadId(long leadId)
        {
            
            if (leadId <= 0) return BadRequest("Lead was not found");
            var response = await _httpClient.GetAsync($"https://localhost:44388/transaction/by-lead-id/{leadId}");
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionOutputModel>> GetTransactionById(long id)
        {
            if (id <= 0) return BadRequest("Lead was not found");
            var response = await _httpClient.GetAsync($"https://localhost:44388/transaction/{id}");
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{leadId}/balance/{currencyId}")]
        public async Task<ActionResult<decimal>> GetBalanceByLeadIdInCurrency(long leadId, byte currencyId)
        {
            if (leadId <= 0) return BadRequest("Lead was not found");
            if (currencyId <= 0) return BadRequest("Currency was not found");
            var response = await _httpClient.GetAsync($"https://localhost:44388/transaction/{leadId}/balance/{currencyId}");
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }
    }
}
