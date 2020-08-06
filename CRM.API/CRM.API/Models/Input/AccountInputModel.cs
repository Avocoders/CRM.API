using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.API.Models.Input
{
    public class AccountInputModel
    {
		public long? Id { get; set; }
		
		public long LeadId { get; set; }
				
		public byte CurrencyId { get; set; }
	


    }
}
