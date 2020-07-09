using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD.TataBuku.Ledger.API.Business.Purchase
{
    public class Response
    {
        public string msg { get; set; }
        public bool result { get; set; }
        public bool success { get; set; }
        public int severity { get; set; }
    }
}
