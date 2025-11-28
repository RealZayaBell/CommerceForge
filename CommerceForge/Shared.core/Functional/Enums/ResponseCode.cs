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

    [Description("Invalid details supplied")]
    ValidationError = 1,

    [Description("No record found")]
    NotFound = 2,

    [Description("Request failed. Please try again")]
    Failed = 3,

    [Description("Authentication failed. Please try again with the right credentials")]
    AuthorizationError = 4,

    [Description("The tenant is disabled")]
    TenantDisabled = 5,
}

