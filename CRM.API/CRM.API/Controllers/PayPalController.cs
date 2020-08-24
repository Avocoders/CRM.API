using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using RestSharp;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using CRM.Core;
using CRM.Data;
using CRM.API.Models.Input;

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
        //[HttpPost("token")]

        //public async Task<ActionResult<string>> CreatePayPalPayment([FromBody] TokenInputModel tokenInputModel)
        //{
        //}

        //internal string GenerateServerBasedAccessToken()
        //{
        //    var result = _http.GeneralRequest($"{BaseOauthToken}?client_id={Settings.ClientId}&client_secret={Settings.Secret}&grant_type=client_credentials", "POST", null, ApiVersion.Helix, Settings.ClientId, null);
        //    if (result.Key == 200)
        //    {
        //        var user = JsonConvert.DeserializeObject<dynamic>(result.Value);
        //        var offset = (int)user.expires_in;
        //        return (string)user.access_token;
        //    }
        //    return null;
        //}


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
