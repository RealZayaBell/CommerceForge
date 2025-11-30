using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.core.Functional.Enums;

public enum ResponseCode
{
    [Description("Request was successful")]
    Ok = 0,

    [Description("Resource Created")]
    Created = 1,

    [Description("Request Accepted")]
    Accepted = 2,

    [Description("Invalid details supplied")]
    ValidationError,

    [Description("No record found")]
    NotFound,

    [Description("Request failed. Please try again")]
    Failed,

    [Description("Authentication failed. Please try again with the right credentials")]
    AuthorizationError,

    [Description("The tenant is disabled")]
    TenantDisabled,
}

