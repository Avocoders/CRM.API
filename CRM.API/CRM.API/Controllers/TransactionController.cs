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
using CRM.API.Models;
using AutoMapper;
using CRM.Data.DTO;

namespace CRM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {        
        private readonly RestClient _restClient;
        private readonly ILeadRepository _repo;
        private readonly IOperationRepository _operation;
        private static long operationId; 

        // private readonly ILogger _logger;
        private readonly GoogleAuthentication _authentication;
        private readonly IMapper _mapper;


        public TransactionController(ILeadRepository repo, IOperationRepository operation, IOptions<UrlOptions> options, IMapper mapper)
        {                        
            _repo = repo;            
            _restClient = new RestClient(options.Value.TransactionStoreAPIUrl);
            // _logger = logger;
            _authentication = new GoogleAuthentication();
            _mapper = mapper;
            _operation = operation;
        }        

        /// <summary>
        /// refers to TransactionStore to create a transfer transaction
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("transfer")]
        public async Task<ActionResult<List<long>>> CreateTransferTransaction([FromBody] TransferInputModel transactionModel)
        {
            if (_repo.GetAccountById(transactionModel.AccountId).Data is null) return BadRequest("The account is not found");
            if (_repo.GetAccountById(transactionModel.AccountIdReceiver).Data is null) return BadRequest("The account of receiver is not found");
            if (transactionModel.Amount <= 0) return BadRequest("The amount is missing");
            transactionModel.CurrencyId = _repo.GetCurrencyByAccountId(transactionModel.AccountId).Data;
            transactionModel.ReceiverCurrencyId = _repo.GetCurrencyByAccountId(transactionModel.AccountIdReceiver).Data;
            var restRequest = new RestRequest("transaction/transfer", Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(transactionModel);
            //string code = Convert.ToString((CurrenciesCode)transactionModel.CurrencyId.Value);
            //_logger.LogInformation($"Create new TransferTransaction from Account {transactionModel.AccountId} to Account {transactionModel.AccountIdReceiver}: " +
            //                       $"{transactionModel.Amount} {code}");
            var result = _restClient.Execute<List<long>>(restRequest);
            return MakeResponse(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]      
        [HttpPost("withdraw/authentication")]
        public async Task<ActionResult<AuthModel>> CreateWithdrawTransaction1([FromBody] TransactionInputModel transactionModel)
        {
            if (_repo.GetAccountById(transactionModel.AccountId).Data is null) return BadRequest("The account is not found");
            if (transactionModel.Amount <= 0) return BadRequest("The amount is missing");
            //var validateInputModel = new ValidatorOfTransactionModel();
            //validateInputModel.ValidateTransactionInputModel(transactionModel);                         
            _authentication.GenerateTwoFactorAuthentication();
            var model = _mapper.Map<OperationDto>(transactionModel);
            AuthModel auth = new AuthModel();
            operationId= _operation.AddOperation(_mapper.Map<OperationDto>(transactionModel)).Data;
            auth.Id = operationId;
            auth.AuthenticationManualCode = _authentication.AuthenticationManualCode;
            return auth;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("withdraw")]
        public  async Task<ActionResult<long>> CreateWithdrawTransaction2(string pin)
        {            
            if(_authentication.ValidateTwoFactorPIN(pin) == true)
            {
                var transactionModel = _mapper.Map<TransactionInputModel>(_operation.GetOperationById(operationId));
                transactionModel.CurrencyId = _repo.GetCurrencyByAccountId(transactionModel.AccountId).Data;
                var restRequest = new RestRequest("transaction/withdraw", Method.POST, DataFormat.Json);             
                restRequest.AddJsonBody(transactionModel);
                //string code = Convert.ToString((CurrenciesCode)transactionModel.CurrencyId.Value);
                //_logger.LogInformation($"Create new WithdrawTransaction for Account {transactionModel.AccountId}: " +
                //                       $"{transactionModel.Amount} {code}");
                var result = _restClient.Execute<long>(restRequest);

                return MakeResponse(result);
            }
            return BadRequest("((((");
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
                //string code = Convert.ToString((CurrenciesCode)transactionModel.CurrencyId.Value);
                //_logger.LogInformation($"Create new DepositTransaction for Account {transactionModel.AccountId}: " +
                //                       $"{transactionModel.Amount} {code}");
                var result = _restClient.Execute<long>(restRequest);
                return MakeResponse(result);
         

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
             DataWrapper<int> dataWrapper = _repo.AccountFindById(accountId);
             if (dataWrapper.Data == 0) return BadRequest("The account is not found or was deleted");  
            var restRequest = new RestRequest($"transaction/by-account-id/{accountId}", Method.GET, DataFormat.Json);
            var result = _restClient.Execute<List<TransactionOutputModel>>(restRequest);
            return MakeResponse(result);        
        }

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
            var restRequest = new RestRequest($"transaction/{id}", Method.GET, DataFormat.Json);
            var result = _restClient.Execute<List<TransactionOutputModel>>(restRequest);
            return MakeResponse(result);
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
            DataWrapper<int> dataWrapper = _repo.AccountFindById(accountId);
            if (dataWrapper.Data == 0) return BadRequest("The account is not found or was deleted");
            var restRequest = new RestRequest($"transaction/{accountId}/balance", Method.GET, DataFormat.Json);
            var result = _restClient.Execute<decimal>(restRequest);
            return  MakeResponse(result);
        }
        
        private ActionResult<T> MakeResponse<T>(IRestResponse<T> result)
        {           
            if (result.StatusCode == 0)
            {
                return Problem(result.ErrorException.InnerException?.Message ?? result.ErrorException.Message, statusCode: 503); 
            }
            //if ((int)result.StatusCode == 418)// придумать код согласовано с TS
            //{
            //    return Problem("Not enough money on the account", statusCode: 520);
            //}
            return Ok(result.Data);
        }
    }
}
