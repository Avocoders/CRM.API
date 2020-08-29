using CRM.API.Models.Input;
using System.Collections.Generic;

namespace CRM.API.Controllers
{
    public class Transaction
    {
        public List<RelatedResource> related_resources { get; set; }
    }
}