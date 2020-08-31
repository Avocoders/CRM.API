using System.Collections.Generic;

namespace CRM.API.Models.Input
{
    public class PaypalInputModel
    {              
            public string intent { get; set; }
            public Payer payer { get; set; }           
            public List<Transactions> transactions { get; set; }            
            public RedirectUrls redirect_urls { get; set; }       
    }

    public class Payer
    {
        public string payment_method { get; set; }
    }

    public class Transactions
    {
        public Amount amount { get; set; }
    }

    public class Amount
    {
        public string total { get; set; }
        public string currency { get; set; }
    }

    public class RedirectUrls
    {
        public string return_url { get; set; } = "https://sandbox.paypal.com";
        public string cancel_url { get; set; } = "https://sandbox.paypal.com";
    }
}
