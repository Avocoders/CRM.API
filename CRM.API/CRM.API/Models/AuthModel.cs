using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.API.Models
{
    public class AuthModel
    {
        public long AccountId { get; set; }
        public byte? CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public string AuthenticationManualCode { get; set; }
    }
}
