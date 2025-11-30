using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.core.Functional;
using System.Diagnostics;
using TenantService.Application.DTOs.Requests;
using TenantService.Application.DTOs.Response;
using TenantService.Application.Services.Interfaces;

namespace TenantService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController(ILogger<TenantsController> logger, ITenantService tenantService) : BaseController
    {

        [HttpPost]
        [ProducesResponseType(typeof(CreateTenantResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTenant(CreateTenantRequest ctr)
        {
            string methodName = "CreateTenant";
            var sw = Stopwatch.StartNew();
            logger.LogInformation("{@methodName} with details {@details}", methodName, ctr);

            var actionResponse = await tenantService.CreateTenant(ctr).ConfigureAwait(false);
            IActionResult result = CustomResponse(actionResponse);

            var timeSpent = sw.Elapsed.TotalMilliseconds;
            logger.LogInformation("{@methodName} ended at {@timeSpent}ms with response => {@response}", methodName, timeSpent, result);
            return result;
        }
    }
}
