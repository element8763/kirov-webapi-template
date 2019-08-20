using BLL.Template;
using Extension.Template.Exceptions;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using NAutowired.Core.Attributes;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Template.Filter
{
    [Filter]
    public class AuthorizationFilter : IAuthorizationFilter, IOperationFilter
    {

        [Autowired]
        private Session session;

        public void OnAuthorization(AuthorizationFilterContext context)
        {



            //身份认证通过
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }
            string token = context.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
#if DEBUG
                return;
#endif
                throw new NoPermissionException();
            }
            //set session value
        }

        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (context.ControllerActionDescriptor.MethodInfo.CustomAttributes.Any(attr => attr.AttributeType == typeof(Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute)))
            {
                return;
            }

            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "token",
                In = "header",
                Type = "string",
                Required = true // set to false if this is optional
            });
        }
    }
}
