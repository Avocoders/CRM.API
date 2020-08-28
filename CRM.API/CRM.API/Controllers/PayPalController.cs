using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionStore.API.Models.Input;
using CRM.API.Models.Output;
using Microsoft.AspNetCore.Http;
using RestSharp;
using Microsoft.Extensions.Options;
using CRM.Core;
using CRM.API.Models;
using CRM.API.Models.Input;
using System;
using System.Globalization;
using RestSharp.Authenticators;
using PayPal.Api;
using PayPal;
using APIContext = PayPal.Api.APIContext;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PayPalController: Controller
    {
        private RestClient _payPalClient;
        private readonly TransactionController _transaction;

        //private readonly ILogger _logger;
        private const string _paymentUrl = "payments/payment";
        private const string _createToken = "oauth2/token";
        private const string userName = "AUQVTtwW6FAGCRUZNVcFU9BffNtzw-ukYIQmW1pk-uODKcB_Y3Ei6NfE25lC8VPwqjFcCMS3pokeQCy_";
        private const string password = "EEGtuAyQIHSYEgmV9VfA7I_7XqaKrY566l1NIJytW8z19Vbp-LiLxxYwNlrpF7Ga-4sLCY7BbX5T9Du1";

        public PayPalController(IOptions<UrlOptions> options, TransactionController transaction  /*, ILogger logger*/)
        {
            _payPalClient = new RestClient(options.Value.PayPalUrl);
            _transaction = transaction;
            //paymentId = _paymentId;
            /*_logger = logger;*/
        }

        [HttpPost("token")]

        public ActionResult<string> GetPayPalToken()
        {
            _payPalClient.Authenticator = new HttpBasicAuthenticator(userName, password);
            var restRequest = new RestRequest($"{_createToken}?grant_type=client_credentials", Method.POST, DataFormat.Json);
            var tmp = _payPalClient.Execute<Token>(restRequest).Data;
            return tmp.Access_Token;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(_paymentUrl)]
        public async Task<ActionResult<string>> CreatePayPalPayment([FromBody] PaypalInputModel paypalInputModel)
        {
            var tmp = GetPayPalToken().Value;
            _payPalClient.AddDefaultHeader("Authorization", $"Bearer {tmp}");
            var restRequest = new RestRequest(_paymentUrl, Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(paypalInputModel);
            var tmp2 = _payPalClient.Execute<PayPalOutputModel>(restRequest).Data;
            try
            {
                Response.Redirect(tmp2.links[1].href);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            Payment.paymentId = tmp2.id;

            return "-1";
        }

        [HttpPost(_paymentUrl + "/execute/{accountId}")]
        public async Task<ActionResult<string>> ExecutePayPalPayment([FromBody] ExecuteInputModel model, long accountId)
        {

            var tmp = GetPayPalToken().Value;
            _payPalClient.AddDefaultHeader("Authorization", $"Bearer {tmp}");
            var restRequest = new RestRequest($"{_paymentUrl}/{Payment.paymentId}/execute/", Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(new { payer_id = model.payerId });
            var tmp2 = _payPalClient.Execute<PaypalInputModel>(restRequest);
            if ((int)tmp2.StatusCode == 200)
            {
                //var tmp4 = (tmp2.Data.transactions[0].amount.total);
                var tmp3 = Decimal.Parse(tmp2.Data.transactions[0].amount.total, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                TransactionInputModel transactionModel = new TransactionInputModel();
                transactionModel.AccountId = accountId;
                transactionModel.Amount = tmp3;
                var result = await _transaction.CreateDepositTransaction(transactionModel);
                if (result.Value!=0) return Ok(result);
                else 
                {
                    Amount refundAmount = new Amount();
                    refundAmount.total = "5";
                    refundAmount.currency = "RUB";
                    Refund refund = new Refund();
                    refund.amount = refundAmount;
                    var apiContext = new APIContext(tmp);
                    string saleId = Payment.paymentId;
                    Refund refundforreal = Sale.Refund(apiContext, saleId, refund);
                    return refundforreal.id;
                    //var refundRequest = new RestRequest($"payments/refun/{Payment.paymentId}", Method.GET, DataFormat.Json);
                    //restRequest.AddJsonBody(new { payer_id = model.payerId });
                    //var tmp4 = _payPalClient.Execute<PaypalInputModel>(refundRequest);                   
                }
            }
            return BadRequest("418.все печально(((");
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
    public static class Payment
    {
        public static string paymentId;
    }
}