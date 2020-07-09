using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DD.TataBuku.Ledger.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class GeneralLedgerController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public GeneralLedgerController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [Route("api/accounting/generalledger/purchase")]
        [HttpPost]
        public async Task<IActionResult> ListPaging([FromBody]Business.Purchase.Request request)
        {
            var result = await _mediator.Send(request);
            return  result.success ? 
                new ObjectResult(new { MessageDescription = result.msg }) { StatusCode = 200 } : 
                new ObjectResult(new { MessageDescription = result.msg }) { StatusCode = 501 };
        }
    }
}