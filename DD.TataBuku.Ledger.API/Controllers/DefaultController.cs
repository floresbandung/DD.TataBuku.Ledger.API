using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DD.TataBuku.Ledger.API.DataContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DD.TataBuku.Ledger.API.Controllers
{
    [Route("/")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly GLDataContext _dataContext;

        public DefaultController(IWebHostEnvironment environment, GLDataContext dataContext)
        {
            _environment = environment;
            _dataContext = dataContext;
        }
        public async Task<IActionResult> DefaultAction()
        {
            dynamic result = new ExpandoObject();
            result.AssemblyName = GetType().AssemblyQualifiedName;
            result.Environment = _environment.EnvironmentName;
            result.DatabaseConnectionAvailable = await _dataContext.Database.CanConnectAsync();
            result.Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
            return Ok( result);
        }
    }
}