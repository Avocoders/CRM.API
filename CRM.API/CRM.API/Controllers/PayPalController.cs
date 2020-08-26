using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using RestSharp;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using CRM.Core;
using CRM.Data;
using CRM.API.Models.Input;
using RestSharp.Authenticators;
using CRM.API.Models;
using Newtonsoft.Json;
using TransactionStore.API.Models.Input;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Globalization;
using CRM.API.Models.Output;
using System.Diagnostics;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PayPalController : TransactionController
    {
        private RestClient _payPalClient;
        private string paymentId;
        //private readonly ILogger _logger;
        private const string _paymentUrl = "payments/payment";
        private const string _createToken = "oauth2/token";
        private const string userName = "AUQVTtwW6FAGCRUZNVcFU9BffNtzw-ukYIQmW1pk-uODKcB_Y3Ei6NfE25lC8VPwqjFcCMS3pokeQCy_";
        private const string password = "EEGtuAyQIHSYEgmV9VfA7I_7XqaKrY566l1NIJytW8z19Vbp-LiLxxYwNlrpF7Ga-4sLCY7BbX5T9Du1";     
        

        public PayPalController(ILeadRepository repo, IOptions<UrlOptions> options/*, ILogger logger*/) : base(repo, options)
        {           
            _payPalClient = new RestClient(options.Value.PayPalUrl);           
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
            catch(Exception ex)
            {
                return ex.Message;
            }
            paymentId = tmp2.id;
            //ExecutePayPalPayment(tmp2.id, )
            return "-1";
        }

        [HttpPost(_paymentUrl + "/{paymentId}/execute/{accountId}")]
        public async void ExecutePayPalPayment (string paymentId, [FromBody] ExecuteInputModel model, long accountId)
        {
            var tmp = GetPayPalToken().Value;            
            _payPalClient.AddDefaultHeader("Authorization", $"Bearer {tmp}");
            var restRequest = new RestRequest($"{_paymentUrl}/{paymentId}/execute/{accountId}", Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(new { payer_id  = model.payerId });
            var tmp2 = _payPalClient.Execute<PaypalInputModel>(restRequest);          
            if((int)tmp2.StatusCode == 200)
            {
                var tmp4 = (tmp2.Data.transactions[0].amount.total);
                var tmp3 = Decimal.Parse(tmp4, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                TransactionInputModel transactionModel = new TransactionInputModel();
                transactionModel.AccountId = accountId;
                transactionModel.Amount = tmp3;
                var result = CreateDepositTransaction(transactionModel);                
            }
          
            //после проверки дописываем отправку в транзакшн стор, если не вернет успех, то рефанд делаем

        }
    }
}
