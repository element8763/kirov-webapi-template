using BLL.Template;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NAutowired.Core.Attributes;
using WebAPI.Template.Filter;

namespace WebAPI.Template.Controllers
{
    [NAutowired.Attributes.ServiceFilter(typeof(AuthorizationFilter))]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 用户
        /// </summary>
        [Autowired]
        protected Session Session { get; set; }

        [Autowired]
        protected readonly ILoggerFactory loggerFactory;

        /// <summary>
        /// 日志
        /// </summary>
        protected ILogger logger { get { return loggerFactory.CreateLogger(GetType().FullName); } }
    }
}