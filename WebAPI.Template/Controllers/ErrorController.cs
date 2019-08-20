using Extension.Template.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebAPI.Template.Model;

namespace WebAPI.Template.Controllers
{
    public class ErrorController : ControllerBase
    {
        protected readonly ILogger<ErrorController> logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("error/catch_all")]
        public async Task<IActionResult> CatchAll()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = feature?.Error;
            logger.LogError("请求头：" + Request.Headers.ToString());
            logger.LogError("错误信息：" + error?.Message);
            logger.LogError("错误堆栈：" + error?.StackTrace);
            //如果是权限错误
            if (error is NoPermissionException)
            {
                return new ObjectResult(new MessageModel
                {
                    Code = "PERMISSION_EXCEPTION",
                    Message = error?.Message
                })
                {
                    StatusCode = 401
                };
            }
            else if (error is BusinessException)
            {
                return new ObjectResult(new MessageModel
                {
                    Code = "BUSINESS_EXCEPTION",
                    Message = error?.Message
                })
                {
                    StatusCode = 400
                };
            }
            return new ObjectResult(new MessageModel
            {
                Code = "SYSTEM_EXCEPTION",
                Message = "ServerError" + ":" + error?.Message
            })
            {
                StatusCode = 400
            };
        }
    }
}
