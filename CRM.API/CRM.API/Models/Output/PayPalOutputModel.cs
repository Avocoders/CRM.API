﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.API.Models.Output
{
    public class PayPalOutputModel
    {
        public string id { get; set; }
        public List<Links> links { get; set; }
    }

    public class Links
    { 
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
    }
}
