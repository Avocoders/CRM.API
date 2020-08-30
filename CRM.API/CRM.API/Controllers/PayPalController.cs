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
using PayPal.Api;
using APIContext = PayPal.Api.APIContext;
using System.Globalization;
using TransactionStore.API.Models.Input;
using NLog;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PayPalController: Controller
    {
        private RestClient _payPalClient;
        private readonly TransactionController _transaction;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private const string _paymentUrl = "payments/payment";
        private const string _createToken = "oauth2/token";
        private const string userName = "AUQVTtwW6FAGCRUZNVcFU9BffNtzw-ukYIQmW1pk-uODKcB_Y3Ei6NfE25lC8VPwqjFcCMS3pokeQCy_";
        private const string password = "EEGtuAyQIHSYEgmV9VfA7I_7XqaKrY566l1NIJytW8z19Vbp-LiLxxYwNlrpF7Ga-4sLCY7BbX5T9Du1";

        public PayPalController(IOptions<UrlOptions> options, TransactionController transaction)
        {
            _payPalClient = new RestClient(options.Value.PayPalUrl);
            _transaction = transaction;         
        }

        public static class Payment  //надо убрать статичный класс
        {
            public static string paymentId;
        }

        [HttpPost("token")]
        public ActionResult<string> GetPayPalToken()
        {
            _payPalClient.Authenticator = new HttpBasicAuthenticator(userName, password);
            var restRequest = new RestRequest($"{_createToken}?grant_type=client_credentials", Method.POST, DataFormat.Json);
            var token = _payPalClient.Execute<Token>(restRequest).Data;
            _logger.Info($"Get PayPal Token");
            return token.Access_Token;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(_paymentUrl)]
        public async Task<ActionResult<string>> CreatePayPalPayment([FromBody] PaypalInputModel payPalInputModel)
        {
            var token = GetPayPalToken().Value;
            _payPalClient.AddDefaultHeader("Authorization", $"Bearer {token}");
            var restRequest = new RestRequest(_paymentUrl, Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(payPalInputModel);
            var payPalOutputModel = _payPalClient.Execute<PayPalOutputModel>(restRequest).Data;
            try
            {
                Response.Redirect(payPalOutputModel.links[1].href);
            }
            catch (Exception ex)
            {
                _logger.Debug(ex.Message);
                return BadRequest(ex.Message);
            }
            Payment.paymentId = payPalOutputModel.id;
            _logger.Info($"Wait to confirm Payment [{Payment.paymentId}]");
            return Ok("Confirm your payment now");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost(_paymentUrl + "/execute/{accountId}")]
        public async Task<ActionResult<string>> ExecutePayPalPayment([FromBody] ExecuteInputModel executeInputModel, long accountId)
        {
            var token = GetPayPalToken().Value;
            _payPalClient.AddDefaultHeader("Authorization", $"Bearer {token}");
            var restRequest = new RestRequest($"{_paymentUrl}/{Payment.paymentId}/execute/", Method.POST, DataFormat.Json);            
            restRequest.AddJsonBody(new { payer_id = executeInputModel.payerId });
            var executeOutputModel = _payPalClient.Execute<ExecuteOutputModel>(restRequest);
            if ((int) executeOutputModel.StatusCode == 200)
            {                
                var totalAmount = Decimal.Parse(executeOutputModel.Data.transactions[0].amount.total, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                TransactionInputModel transactionModel = new TransactionInputModel();
                transactionModel.AccountId = accountId;
                transactionModel.Amount = totalAmount;
                var result = await _transaction.CreateDepositTransaction(transactionModel);
                if (result.Value != 0) return Ok(result);
                else
                {
                    //RefundRequest refund = new RefundRequest();   либо вот это для рефанда???
                    //var apiContext = new APIContext(token);
                    //string saleId = executeOutputModel.Data.transactions[0].related_resources[0].sale.id;
                    //Refund refundforreal = Sale.Refund(apiContext, saleId, refund);

                    var refund = new RestRequest($"payments/sale/{executeOutputModel.Data.transactions[0].related_resources[0].sale.id}/refund", Method.POST, DataFormat.Json);
                    var refundId = _payPalClient.Execute<RefundOutputModel>(refund).Data.id;
                    _logger.Info($"Payment was refund with RefundId [{refundId}]");
                    return BadRequest($"Payment was refund, refundId: {refundId}");
                }
            }
            _logger.Info($"418.все печально(((");
            return BadRequest("418.все печально(((");
        }                

        //private ActionResult<T> MakeResponse<T>(IRestResponse<T> result)  пока не нужен
        //{
        //    if (result.StatusCode == 0)
        //    {
        //        return Problem(result.ErrorException.InnerException?.Message ?? result.ErrorException.Message, statusCode: 503);
        //    }
        //    if ((int)result.StatusCode == 418)
        //    {
        //        return Problem("Not enough money on the account", statusCode: 520);
        //    }
        //    return Ok(result.Data);
        //}
    }    
}