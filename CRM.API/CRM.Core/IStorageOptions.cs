using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core
{
    public interface IStorageOptions
    {
        public string DBConnectionString { get; set; }
    }
}
