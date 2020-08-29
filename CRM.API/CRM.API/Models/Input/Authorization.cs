using PayPal.Api;
using TransactionStore.API.Models.Input;

namespace CRM.API.Models.Input
{
    public class Sale
    {
        public string id { get; set; }
        public string state { get; set; }       
        public string reason_code { get; set; }
        public string protection_eligibility { get; set; }
        public string parent_payment { get; set; }   
    }
}