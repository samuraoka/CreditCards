using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CreditCards.Web.Controllers
{
    // Combining routes - Routing to controller actions in ASP.NET Core
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-2.0#combining-routes
    [Route("values")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid request for id {id}");
            }

            return Content($"Value {id}");
        }

        // Invoke-WebRequest
        // https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.utility/invoke-webrequest?view=powershell-6
        // Invoke-WebRequest -Uri http://localhost:58924/values/StartJob -Method POST
        [HttpPost("StartJob")]
        public IActionResult StartJob()
        {
            return Ok("Batch Job Started");
        }
    }
}
