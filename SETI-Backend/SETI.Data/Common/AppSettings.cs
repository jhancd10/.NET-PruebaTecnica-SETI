﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETI.Data.Common
{
    public class AppSettings
    {
        public string AdoConnectionString { get; set; }
        public string TokenSecretKey { get; set; }
        public int TokenExpiration { get; set; }
    }
}
