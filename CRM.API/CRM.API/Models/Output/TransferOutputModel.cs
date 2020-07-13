using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.API.Models.Output
{
    public class TransferOutputModel : TransactionOutputModel
    {
        public long TransientLeadId { get; set; }
    }
}
