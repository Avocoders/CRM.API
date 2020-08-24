using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionStore.API.Models.Input;

namespace CRM.API.Models.Input
{
    public class PaypalInputModel
    {              
            public string intent { get; set; }
            public PayerInputModel payer { get; set; }
           
            public List<TransactionsInputModel> transactions { get; set; }
            
            public Redirect_Urls redirect_urls { get; set; }
        // id Lead, id account ,
        
     }

}
