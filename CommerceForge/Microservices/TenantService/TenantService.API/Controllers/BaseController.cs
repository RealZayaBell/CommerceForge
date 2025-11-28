using Microsoft.AspNetCore.Mvc;
using Shared.core.Functional;
using Shared.core.Functional.Enums;
using Shared.core.Functional.Extensions;

namespace TenantService.Controllers
{
    public class BaseController : ControllerBase
    {
        public readonly string FailureMessage = "Request failed, please try again";

        protected IActionResult CustomResponse<T>(ActionResponse<T> result)
        {
            try
            {
                switch (result.Response)
                {
                    case ResponseCode.Ok:
                        {
                            result.Message = string.IsNullOrEmpty(result.Message) ? ResponseCode.Ok.ToDescription() : result.Message;
                            return Ok(result);
                        }
                    case ResponseCode.Failed:
                        {
                            result.Message = string.IsNullOrEmpty(result.Message) ? FailureMessage : result.Message;
                            return BadRequest(result);
                        }
                    case ResponseCode.ValidationError:
                        {
                            result.Message = string.IsNullOrEmpty(result.Message) ? FailureMessage : result.Message;
                            return BadRequest(result);
                        }

                    case ResponseCode.AuthorizationError:
                        {
                            result.Message = ResponseCode.AuthorizationError.ToDescription();
                            return Unauthorized(result);
                        }

                    case ResponseCode.NotFound:
                        {
                            result.Message = string.IsNullOrEmpty(result.Message) ? ResponseCode.NotFound.ToDescription() : result.Message;
                            return NotFound(result);
                        }
                    case ResponseCode.TenantDisabled:
                        {
                            result.Message = string.IsNullOrEmpty(result.Message) ? ResponseCode.TenantDisabled.ToDescription() : result.Message;
                            return Ok(result);
                        }
                    default:
                        {
                            result.Message = GenerateMessage(result.Message, FailureMessage);
                            result.Message = GenerateMessage(result.Message, FailureMessage);
                            return BadRequest(result);
                        }
                }
            }
            catch (Exception)
            {
                result.Message = GenerateMessage(result.Message, FailureMessage);

                result.Message = GenerateMessage(result.Message, FailureMessage);
                return BadRequest(result);
            }
        }

        private string GenerateMessage(string FieldMessage, string Response)
        {
            return string.IsNullOrEmpty(FieldMessage) ? Response : FieldMessage;
        }
    }
}
