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

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PayPalController : Controller
    {
        private RestClient _payPalClient;
        private readonly ILeadRepository _repo;
        //private readonly ILogger _logger;
        private const string _paymentUrl = "payments/payment";
        private const string _createToken = "oauth2/token";
        private const string userName = "AUQVTtwW6FAGCRUZNVcFU9BffNtzw-ukYIQmW1pk-uODKcB_Y3Ei6NfE25lC8VPwqjFcCMS3pokeQCy_";
        private const string password = "EEGtuAyQIHSYEgmV9VfA7I_7XqaKrY566l1NIJytW8z19Vbp-LiLxxYwNlrpF7Ga-4sLCY7BbX5T9Du1";
        private readonly string _options;

        public PayPalController(ILeadRepository repo, IOptions<UrlOptions> options/*, ILogger logger*/)
        {
            _repo = repo;
            _payPalClient = new RestClient(options.Value.PayPalUrl);
            _options = options.Value.PayPalUrl;
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
            _payPalClient = new RestClient(_options);
            _payPalClient.AddDefaultHeader("Authorization", $"Bearer {tmp}");
            var restRequest = new RestRequest(_paymentUrl, Method.POST, DataFormat.Json);
            //restRequest.AddHeader("Authorization", "Bearer " + tmp.Value);
            restRequest.AddJsonBody(paypalInputModel);
            var tmp2 = _payPalClient.Execute<string>(restRequest).Data;
            return Ok();           
        }

    }
}
