using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionStore.API.Models.Input
{
    public class AmountInputModel
    {
         public string total { get; set; }
         public string currency { get; set; }
    }
}
