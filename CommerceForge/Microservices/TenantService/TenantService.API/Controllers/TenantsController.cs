using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.core.Functional;
using System.Diagnostics;
using TenantService.Application.DTOs.Requests;

namespace TenantService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController(ILogger<TenantsController> logger) : BaseController
    {
        private readonly ILogger<TenantsController> _logger;

        [HttpPost]
        public IActionResult CreateTenant(CreateTenantRequest ctr)
        {
            //string methodName = "AddUsersToApp";
            //var sw = Stopwatch.StartNew();
            //_logger.LogInformation("{@methodName} with details {@details}", methodName, ctr);

            //var actionResponse = await _tenantService.CreateTenant(ctr).ConfigureAwait(false);
            //IActionResult result = CustomResponse(actionResponse);

            //var timeSpent = sw.Elapsed.TotalMilliseconds;
            //_logger.LogInformation("{@methodName} ended at {@timeSpent}ms with response => {@response}", methodName, timeSpent, result);
            //return result;
            return Ok();
        }
    }
}
