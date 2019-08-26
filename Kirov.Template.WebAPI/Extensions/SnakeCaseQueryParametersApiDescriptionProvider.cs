using Extension.Template;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Linq;

namespace WebAPI.Template
{
    class SnakeCaseQueryParametersApiDescriptionProvider : IApiDescriptionProvider
    {
        public void OnProvidersExecuting(ApiDescriptionProviderContext context) { }

        public void OnProvidersExecuted(ApiDescriptionProviderContext context)
        {
            foreach (var parameter in context.Results.SelectMany(x => x.ParameterDescriptions)
              .Where(x => x.Source.Id == "Query"))
            {
                parameter.Name = parameter.Name.ToSnakeCase();
            }
        }

        public int Order
        {
            get { return 1; }
        }
    }
}