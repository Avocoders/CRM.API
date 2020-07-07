using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.API.Models.Input
{
    public class AuthorizeInputModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
