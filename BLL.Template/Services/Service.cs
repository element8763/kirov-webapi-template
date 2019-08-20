using Microsoft.Extensions.Logging;
using NAutowired.Core.Attributes;
using Snowflake.Core;

namespace BLL.Template.Services
{
    public class Service
    {
        /// <summary>
        /// 
        /// </summary>
        [Autowired]
        protected readonly Session session;

        [Autowired]
        protected readonly IdWorker idWorker;

        [Autowired]
        protected readonly ILoggerFactory loggerFactory;

        /// <summary>
        /// 日志
        /// </summary>
        protected ILogger logger { get { return loggerFactory.CreateLogger(GetType().FullName); } }
    }
}
