using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.DTO
{
    public class OperationDto
    {
        public long? Id { get; set; }
        public long? LeadId { get; set; }
        public long AccountId { get; set; }
        public decimal Amount { get; set; }
        public bool? IsCompleted { get; set; }
    }
}

