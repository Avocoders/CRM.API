using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRM.API.Models.Output;
using Microsoft.AspNetCore.Http;
using RestSharp;
using Microsoft.Extensions.Options;
using CRM.Core;
using CRM.API.Models;
using CRM.API.Models.Input;
using System;
using RestSharp.Authenticators;
using System.Globalization;
using TransactionStore.API.Models.Input;
using NLog;
using CRM.API.Options;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PayPalController: Controller
    {
        private RestClient _payPalClient;
        private readonly TransactionController _transaction;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();        

        public PayPalController(IOptions<UrlOptions> options, TransactionController transaction)
        {
            _payPalClient = new RestClient(options.Value.PayPalUrl);
            _transaction = transaction;         
        }

        /// <summary>
        /// Gets token
        /// </summary>
        /// <returns></returns>
        [HttpPost("token")]
        public ActionResult<string> GetPayPalToken()
        {
            _payPalClient.Authenticator = new HttpBasicAuthenticator(PaypalOptions.userName, PaypalOptions.password);
            var restRequest = new RestRequest($"{PaypalOptions.createToken}?grant_type=client_credentials", Method.POST, DataFormat.Json);
            var token = _payPalClient.Execute<Token>(restRequest).Data;
            _logger.Info($"Get PayPal Token");
            return token.Access_Token;
        }

        /// <summary>
        /// Creates PayPal Payment
        /// </summary>
        /// <param name="payPalInputModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(PaypalOptions.paymentUrl)]
        public async Task<ActionResult<string>> CreatePayPalPayment([FromBody] PaypalInputModel payPalInputModel)
        {
            var token = GetPayPalToken().Value;
            _payPalClient.AddDefaultHeader("Authorization", $"Bearer {token}");
            var restRequest = new RestRequest(PaypalOptions.paymentUrl, Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(payPalInputModel);
            var tmp = await _payPalClient.ExecuteAsync<PayPalOutputModel>(restRequest);
            var payPalOutputModel = tmp.Data;
            try
            {
                Response.Redirect(payPalOutputModel.links[1].href);
            }
            catch (Exception ex)
            {
                _logger.Debug(ex.Message);
                return BadRequest(ex.Message);
            }
            PaypalOptions.paymentId = payPalOutputModel.id;
            _logger.Info($"Wait to confirm Payment [{PaypalOptions.paymentId}]");
            return Ok("Confirm your payment now");
        }

        /// <summary>
        /// Executes payment, which we created
        /// </summary>
        /// <param name="executeInputModel"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost(PaypalOptions.paymentUrl + "/execute/{accountId}")]
        public async Task<ActionResult<string>> ExecutePayPalPayment([FromBody] ExecuteInputModel executeInputModel, long accountId)
        {
            var token = GetPayPalToken().Value;
            _payPalClient.AddDefaultHeader("Authorization", $"Bearer {token}");
            var restRequest = new RestRequest($"{PaypalOptions.paymentUrl}/{PaypalOptions.paymentId}/execute/", Method.POST, DataFormat.Json);            
            restRequest.AddJsonBody(new { payer_id = executeInputModel.payerId });
            var executeOutputModel = _payPalClient.Execute<ExecuteOutputModel>(restRequest);
            if ((int) executeOutputModel.StatusCode == 200)
            {                
                var totalAmount = Decimal.Parse(executeOutputModel.Data.transactions[0].amount.total, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                var transactionModel = new TransactionInputModel();
                transactionModel.AccountId = accountId;
                transactionModel.Amount = totalAmount;
                var result = await _transaction.CreateDepositTransaction(transactionModel);                
                if (result.Value == 0) return Ok("Операция выполнена");
                else
                {
                    var refund = new RestRequest($"payments/sale/{executeOutputModel.Data.transactions[0].related_resources[0].sale.id}/refund", Method.POST, DataFormat.Json);
                    var refundId = _payPalClient.Execute<RefundOutputModel>(refund).Data.id;
                    _logger.Info($"Payment was refund with RefundId [{refundId}]");
                    return BadRequest($"Payment was refund, refundId: {refundId}");
                }
            }
            _logger.Info($"418.все печально(((");
            return BadRequest("418.все печально(((");
        }   
    }    
}