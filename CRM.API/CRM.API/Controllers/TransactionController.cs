﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionStore.API.Models.Input;
using CRM.Data;
using CRM.API.Models.Output;
using Microsoft.AspNetCore.Http;
using RestSharp;
using Microsoft.Extensions.Options;
using CRM.Core;
using CRM.API.Models;
using AutoMapper;
using CRM.Data.DTO;
using CRM.API.Models.Input;
using NLog;
using System;
using Microsoft.AspNetCore.Authorization;

namespace CRM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {        
        private readonly RestClient _restClient;
        private readonly ILeadRepository _repo;
        private readonly IOperationRepository _operation;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly GoogleAuthentication _authentication;
        private readonly IMapper _mapper;

        public TransactionController(ILeadRepository repo, IOperationRepository operation, IOptions<UrlOptions> options, IMapper mapper)
        {                        
            _repo = repo;            
            _restClient = new RestClient(options.Value.TransactionStoreAPIUrl);
            _authentication = new GoogleAuthentication();
            _mapper = mapper;
            _operation = operation;
        }

        /// <summary>
        /// Refers to TransactionStore to create a transfer transaction
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("transfer")]
        public async ValueTask<ActionResult<List<long>>> CreateTransferTransaction([FromBody] TransferInputModel transactionModel)
        {
            var checkingAccountId = await _repo.GetAccountById(transactionModel.AccountId);
            if  (checkingAccountId.Data is null) return BadRequest("The account is not found");
            var checkingAccountIdReceiver = await _repo.GetAccountById(transactionModel.AccountIdReceiver);
            if (checkingAccountIdReceiver.Data is null) return BadRequest("The account of receiver is not found");
            if (transactionModel.Amount <= 0) return BadRequest("The amount is missing");
            var currencyId = await _repo.GetCurrencyByAccountId(transactionModel.AccountId);
            transactionModel.CurrencyId =  currencyId.Data;
            var receiver = await _repo.GetCurrencyByAccountId(transactionModel.AccountIdReceiver);
            transactionModel.ReceiverCurrencyId = receiver.Data;
            var restRequest = new RestRequest("transaction/transfer", Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(transactionModel);
            var result = await _restClient.ExecuteAsync<List<long>>(restRequest);
            string code = Convert.ToString((CurrenciesCode)transactionModel.CurrencyId.Value);
            _logger.Info($"Create new TransferTransaction from Account {transactionModel.AccountId} to Account {transactionModel.AccountIdReceiver}: " +
                                  $"{transactionModel.Amount} {code}");
            return MakeResponse(result);
        }

        /// <summary>
        /// Refers to TransactionStore to create a withdraw transaction before authentification
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]      
        [HttpPost("withdraw/authentication")]
        public async ValueTask<ActionResult<AuthOutputModel>> CreateWithdrawTransactionStepOne([FromBody] TransactionInputModel transactionModel)
        {
            var checkingAccountId = await _repo.GetAccountById(transactionModel.AccountId);
            if (checkingAccountId.Data is null) return BadRequest("The account is not found");
            if (transactionModel.Amount <= 0) return BadRequest("The amount is missing");                    
            _authentication.GenerateTwoFactorAuthentication(transactionModel.AccountId);
            AuthOutputModel auth = new AuthOutputModel();
            var tmp = await _operation.AddOperation(_mapper.Map<OperationDto>(transactionModel));
            auth.Id = tmp.Data;
            auth.AuthenticationManualCode = _authentication.AuthenticationManualCode;
            return auth;
        }

        /// <summary>
        /// Refers to TransactionStore to create a withdraw transaction after authentification
        /// </summary>
        /// <param name="authInput"></param>
        /// <returns></returns>
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("withdraw")]
        public async ValueTask<ActionResult<long>> CreateWithdrawTransaction2([FromBody] AuthInputModel authInput )
        {
            if (authInput.Pin.Length != 6) BadRequest("PIN not entered or incorrect number of characters entered");
            var tmp = await _operation.GetOperationById(authInput.Id);
            var operationModel = tmp.Data;
            var accountId = operationModel.AccountId;
            if (_authentication.ValidateTwoFactorPIN(accountId,authInput.Pin) == true)
            {
                if (operationModel.IsCompleted == false) 
                {
                   await _operation.CompletedOperation(authInput.Id);
                    var transactionModel = _mapper.Map<TransactionInputModel>(operationModel);
                    var currencyId = await _repo.GetCurrencyByAccountId(transactionModel.AccountId);
                    transactionModel.CurrencyId = currencyId.Data;
                    var restRequest = new RestRequest("transaction/withdraw", Method.POST, DataFormat.Json);
                    restRequest.AddJsonBody(transactionModel);
                    var result = await _restClient.ExecuteAsync<long>(restRequest);
                    string code = Convert.ToString((CurrenciesCode)transactionModel.CurrencyId.Value);
                    _logger.Info($"Create new WithdrawTransaction for Account [{transactionModel.AccountId}] " +
                                  $"{transactionModel.Amount} {code}");
                    return MakeResponse(result);
                }
                return Ok("The operation was performed");
            }
            return BadRequest("Incorrect PIN entered");
        }

        /// <summary>
        /// Refers to TransactionStore to create a deposit transaction
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("deposit")]
        public async ValueTask<ActionResult<long>> CreateDepositTransaction([FromBody] TransactionInputModel transactionModel)
        {
            var checkingAccountId = await _repo.GetAccountById(transactionModel.AccountId);
            if (checkingAccountId.Data is null) return BadRequest("The account is not found");
            if (transactionModel.Amount <= 0) return BadRequest("The amount is missing");
                var currencyId = await _repo.GetCurrencyByAccountId(transactionModel.AccountId);
                transactionModel.CurrencyId = currencyId.Data;
                var restRequest = new RestRequest("transaction/deposit", Method.POST, DataFormat.Json);
                restRequest.AddJsonBody(transactionModel);
                var result = await _restClient.ExecuteAsync<long>(restRequest);
                return MakeResponse(result);
        }

        /// <summary>
        /// Refers to TransactionStore to get lead's transactions by accountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("by-account-id/{accountId}")]
        public async ValueTask<ActionResult<List<TransactionOutputModel>>> GetTransactionsByAccountId(long accountId)
        {
             DataWrapper<int> dataWrapper = await _repo.AccountFindById(accountId);
             if (dataWrapper.Data == 0) return BadRequest("The account is not found or was deleted");  
             var restRequest = new RestRequest($"transaction/by-account-id/{accountId}", Method.GET, DataFormat.Json);
             var result = await _restClient.ExecuteAsync<List<TransactionOutputModel>>(restRequest);
             return MakeResponse(result);        
        }

        /// <summary>
        /// Refers to TransactionStore to get transaction by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [HttpGet("{id}")]
        public async ValueTask<ActionResult<List<TransactionOutputModel>>> GetTransactionById(long id)
        {                   
            var restRequest = new RestRequest($"transaction/{id}", Method.GET, DataFormat.Json);
            var result =await _restClient.ExecuteAsync<List<TransactionOutputModel>>(restRequest);
            return MakeResponse(result);
        }

        /// <summary>
        /// Delete all transaction (for tests)
        /// </summary>        
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        public async ValueTask DeleteAllTransaction()
        {
            var restRequest = new RestRequest($"transaction", Method.DELETE, DataFormat.Json);
            await _restClient.ExecuteAsync(restRequest);
        }

        /// <summary>
        /// Refers to TransactionStore to get balance by accountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{accountId}/balance")]
        public async ValueTask<ActionResult<BalanceOutputModel>> GetBalanceByAccountId(long accountId)
        {
            DataWrapper<int> dataWrapper = await _repo.AccountFindById(accountId);
            if (dataWrapper.Data == 0) return BadRequest("The account is not found or was deleted");
            var restRequest = new RestRequest($"transaction/{accountId}/balance", Method.GET, DataFormat.Json);
            var result = await _restClient.ExecuteAsync<BalanceOutputModel>(restRequest);
            return  MakeResponse(result);
        }
        
        private ActionResult<T> MakeResponse<T>(IRestResponse<T> result)
        {
            if (result.StatusCode == 0)
            {
                return Problem(result.ErrorException.InnerException?.Message ?? result.ErrorException.Message, statusCode: 503);
            }
            if ((int)result.StatusCode == 418)
            {
                return Problem("Not enough money on the account", statusCode: 520);
            }
            return Ok(result.Data);
        }
    }
}
