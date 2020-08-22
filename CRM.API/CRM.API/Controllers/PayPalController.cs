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

        public PayPalController(ILeadRepository repo, IOptions<UrlOptions> options, ILogger logger)
        {
            _repo = repo;
            _restClient = new RestClient(options.Value.PayPalUrl);
            _logger = logger;
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
