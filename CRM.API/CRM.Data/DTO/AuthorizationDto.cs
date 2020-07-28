using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.DTO
{
    public class AuthorizationDto
    {
        public long? Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public RoleDto Role { get; set; }
    }
}
