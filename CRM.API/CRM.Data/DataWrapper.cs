using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data
{
    public class DataWrapper<T>
    {
        public T Data { get; set; }
        public bool IsOk { get; set; } = false;
        public string ExceptionMessage { get; set; }
    }
}
