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

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PayPalController : Controller
    {
        private readonly RestClient _restClient;
        private readonly ILeadRepository _repo;
        private readonly ILogger _logger;
        private const string _paymentUrl = "payments/payment";
        private const string _createToken = "api.sandbox.paypal.com/v1/oauth2/token";

        public PayPalController(ILeadRepository repo, IOptions<UrlOptions> options, ILogger logger)
        {
            _repo = repo;
            _restClient = new RestClient(options.Value.PayPalUrl);
            _logger = logger;
        }

        [HttpPost("token")]

        public async Task<ActionResult<string>> GetPayPalToken()
        {
            _restClient.Authenticator = new HttpBasicAuthenticator(userName, password);
            var restRequest = new RestRequest(_createToken, Method.POST, DataFormat.Json);

        }

     
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(_paymentUrl)]
        public async Task<ActionResult<string>> CreatePayPalPayment([FromBody] PaypalInputModel paypalInputModel)
        {
            var restRequest = new RestRequest(_paymentUrl, Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(paypalInputModel);
            return _restClient.Execute<string>(restRequest).Data; ;
        }

    }
}
