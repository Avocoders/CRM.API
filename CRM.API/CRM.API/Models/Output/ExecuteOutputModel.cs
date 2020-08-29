using CRM.API.Models.Input;
using System.Collections.Generic;

namespace CRM.API.Controllers
{
    public class ExecuteOutputModel
    {
        public List<Transaction> transactions { get; set; }
    }

    public class Transaction
    {
        public Amount amount { get; set; }
        public List<RelatedResource> related_resources { get; set; }
    }

    public class RelatedResource
    {
        public SaleModel sale { get; set; }
    }

    public class SaleModel
    {
        public string id { get; set; }       
    }
}