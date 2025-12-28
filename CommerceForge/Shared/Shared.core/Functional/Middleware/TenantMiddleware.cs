using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.core.Functional.Middleware
{
    public class TenantMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context, TenantContext tenant)
        {
            string tenantHost = context.Request.Host.Host;

            // get tenant schema from tenant service via api

            if (string.IsNullOrEmpty(tenantHost))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid tenant.");
                return;
            }

            tenant.TenantId = 4; // whatever is in Id in getting tenant schema
            tenant.Schema = "simeilar" ?? "dbo";

            await next(context);
        }
    }
}
