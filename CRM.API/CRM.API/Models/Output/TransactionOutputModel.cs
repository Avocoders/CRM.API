using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.API.Models.Output
{
    public class TransactionOutputModel
    {
        public long Id { get; set; }
        public long LeadId { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
