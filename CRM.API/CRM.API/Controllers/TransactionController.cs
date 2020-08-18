using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionStore.API.Models.Input;
using CRM.Data;
using CRM.API.Models.Output;
using Microsoft.AspNetCore.Http;
using RestSharp;
using Microsoft.Extensions.Options;
using CRM.Core;
using System;
using Microsoft.Extensions.Logging;

namespace CRM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {        
        private readonly RestClient _restClient;
        private readonly ILeadRepository _repo;
        private readonly ILogger _logger;
      
        public TransactionController(ILeadRepository repo, IOptions<UrlOptions> options, ILogger logger)
        {                        
            _repo = repo;            
            _restClient = new RestClient(options.Value.TransactionStoreAPIUrl);
            _logger = logger;
        }
        
        /// <summary>
        /// refers to TransactionStore to create a transfer transaction
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]       //контроллер не возвращает badRequest из TransactionStore
        [HttpPost("transfer")]
        public async Task<ActionResult<List<long>>> CreateTransferTransaction([FromBody] TransferInputModel transactionModel)
        {
            if (_repo.GetAccountById(transactionModel.AccountId).Data is null) return BadRequest("The account is not found");//
            if (_repo.GetAccountById(transactionModel.AccountIdReceiver).Data is null) return BadRequest("The account of receiver is not found");//
            if (transactionModel.Amount <= 0) return BadRequest("The amount is missing"); //validatetransactionmodel

            transactionModel.CurrencyId = _repo.GetCurrencyByAccountId(transactionModel.AccountId).Data;
            transactionModel.ReceiverCurrencyId = _repo.GetCurrencyByAccountId(transactionModel.AccountIdReceiver).Data;            
            var restRequest = new RestRequest("transaction/transfer", Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(transactionModel);
            restRequest.OnBeforeDeserialization = r => { r.ContentType = "application/json"; };
            string code = Convert.ToString((CurrenciesCode)transactionModel.CurrencyId.Value);
            _logger.LogInformation($"Create new TransferTransaction from Account {transactionModel.AccountId} to Account {transactionModel.AccountIdReceiver}: " +
                                   $"{transactionModel.Amount} {code}");
            return _restClient.Execute<List<long>>(restRequest).Data; 
        }

        /// <summary>
        /// refers to TransactionStore to create a withdraw transaction
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]       //контроллер не возвращает badRequest из TransactionStore (not enough money)
        [HttpPost("withdraw")]
        public async Task<ActionResult<long>> CreateWithdrawTransaction([FromBody] TransactionInputModel transactionModel)
        {
            if (_repo.GetAccountById(transactionModel.AccountId).Data is null) return BadRequest("The account is not found");//validate
            if (transactionModel.Amount <= 0) return BadRequest("The amount is missing");//validate
            transactionModel.CurrencyId = _repo.GetCurrencyByAccountId(transactionModel.AccountId).Data;            
            var restRequest = new RestRequest("transaction/withdraw", Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(transactionModel);
            restRequest.OnBeforeDeserialization = r => { r.ContentType = "application/json"; };
            string code = Convert.ToString((CurrenciesCode)transactionModel.CurrencyId.Value);
            _logger.LogInformation($"Create new WithdrawTransaction for Account {transactionModel.AccountId}: " +
                                   $"{transactionModel.Amount} {code}");
            return _restClient.Execute<long>(restRequest).Data;
        }

        /// <summary>
        /// refers to TransactionStore to create a deposit transaction
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("deposit")]
        public async Task<ActionResult<long>> CreateDepositTransaction([FromBody] TransactionInputModel transactionModel)
        {
            if (_repo.GetAccountById(transactionModel.AccountId).Data is null) return BadRequest("The account is not found");
            if (transactionModel.Amount <= 0) return BadRequest("The amount is missing");
            transactionModel.CurrencyId = _repo.GetCurrencyByAccountId(transactionModel.AccountId).Data;            
            var restRequest = new RestRequest("transaction/deposit", Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(transactionModel);
            restRequest.OnBeforeDeserialization = r => { r.ContentType = "application/json"; };
            string code = Convert.ToString((CurrenciesCode)transactionModel.CurrencyId.Value);
            _logger.LogInformation($"Create new DepositTransaction for Account {transactionModel.AccountId}: " +
                                   $"{transactionModel.Amount} {code}");
            return _restClient.Execute<long>(restRequest).Data; ;
        }

        /// <summary>
        /// refers to TransactionStore to get all lead's transactions by leadId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("by-account-id/{accountId}")]
        public async Task<ActionResult<List<TransactionOutputModel>>> GetTransactionsByAccountId(long accountId)
        {
           // if (accountId <= 0) return BadRequest("Account was not found");   проверку  аккаунт есть и он не удален      
            var restRequest = new RestRequest($"transaction/by-account-id/{accountId}", Method.GET, DataFormat.Json);
            var result = _restClient.Execute<List<TransactionOutputModel>>(restRequest);
            return MakeResponse<List<TransactionOutputModel>>(result);        }

        /// <summary>
        /// refers to TransactionStore to get transaction by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<TransactionOutputModel>>> GetTransactionById(long id)
        {
           // if (id <= 0) return BadRequest("Transaction was not found");            
            var restRequest = new RestRequest($"transaction/{id}", Method.GET, DataFormat.Json);
            var result = _restClient.Execute<List<TransactionOutputModel>>(restRequest);
            return MakeResponse<List<TransactionOutputModel>>(result);
        }

        /// <summary>
        /// refers to TransactionStore to get balance by leadId and currencyId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{accountId}/balance")]
        public async Task<ActionResult<decimal>> GetBalanceByAccountIdInCurrency(long accountId)
        {
            if (accountId <= 0) return BadRequest("Account was not found");       //переделать как в предыдущем // проверку  аккаунт есть и он не удален    
            var restRequest = new RestRequest($"transaction/{accountId}/balance", Method.GET, DataFormat.Json);
            var result = _restClient.Execute<decimal>(restRequest);
            return  MakeResponse<decimal>(result);
        }
        
        private ActionResult<T> MakeResponse<T>(IRestResponse<T> result)
        {           
            if (result.StatusCode == 0)
            {
                return Problem(result.ErrorException.InnerException?.Message ?? result.ErrorException.Message, statusCode: 503); 
            }
            if ((int)result.StatusCode == 418 )// придумать код согласовано с TS
            {
                return Problem("Not enough money on the account", statusCode: 520); 
            }
            return Ok(result.Data);
        }
    }
}
