using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD.TataBuku.Ledger.API.Business.Purchase
{
    public class Request : IRequest<Response>
    {
        public string Subject { get; set; }
        public string Reference { get; set; }
        public decimal Ammount { get; set; }
        public string Username { get; set; }
    }
}
