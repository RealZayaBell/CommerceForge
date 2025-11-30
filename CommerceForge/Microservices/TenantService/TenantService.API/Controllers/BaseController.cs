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
                            result.Message = ChooseMessage(ResponseCode.Ok, result);
                            return Ok(result);
                        }
                    case ResponseCode.Created:
                        {
                            result.Message = ChooseMessage(ResponseCode.Created, result);
                            return Created(new Uri(""), result);
                        }
                    case ResponseCode.Accepted:
                        {
                            result.Message = ChooseMessage(ResponseCode.Accepted, result);
                            return Accepted(result);
                        }

                    case ResponseCode.Failed:
                        {
                            result.Message = ChooseMessage(ResponseCode.Failed, result);
                            return BadRequest(result);
                        }
                    case ResponseCode.ValidationError:
                        {
                            result.Message = ChooseMessage(ResponseCode.ValidationError, result);
                            return BadRequest(result);
                        }

                    case ResponseCode.AuthorizationError:
                        {
                            result.Message = ChooseMessage(ResponseCode.AuthorizationError, result);
                            return Unauthorized(result);
                        }

                    case ResponseCode.NotFound:
                        {
                            result.Message = ChooseMessage(ResponseCode.NotFound, result);
                            return NotFound(result);
                        }
                    case ResponseCode.TenantDisabled:
                        {
                            result.Message = ChooseMessage(ResponseCode.TenantDisabled, result);
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

        private static string ChooseMessage<T>(ResponseCode code, ActionResponse<T> result)
        {
            return string.IsNullOrEmpty(result.Message) ? code.ToDescription() : result.Message;
        }
        private static string GenerateMessage(string FieldMessage, string Response)
        {
            return string.IsNullOrEmpty(FieldMessage) ? Response : FieldMessage;
        }
    }
}
