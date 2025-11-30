using Shared.core.Functional.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.core.Functional;

public class ActionResponse
{
    public ActionResponse()
    {
    }

    public ResponseCode Response { get; set; }
    public string Message { get; set; }
    public List<string> FailureReasons { get; set; }
}

public class ActionResponse<T> : ActionResponse
{
    public T Data { get; set; }

    public ActionResponse<object> ToObject()
    {
        return new ActionResponse<object>
        {
            Response = this.Response,
            Message = this.Message,
            FailureReasons = this.FailureReasons,
            Data = this.Data
        };
    }

    public static ActionResponse<T> Success(T result, string message)
    {
        ActionResponse<T> response = new() { Response = ResponseCode.Ok, Data = result, Message = message };

        return response;
    }

    public static ActionResponse<T> Failed(string errorMessage)
    {
        ActionResponse<T> response = new() { Response = ResponseCode.Failed, Message = errorMessage };

        return response;
    }
}
