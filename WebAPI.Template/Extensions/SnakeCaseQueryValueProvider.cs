using Extension.Template;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace WebAPI.Template
{
    public class SnakeCaseQueryValueProvider : QueryStringValueProvider, IValueProvider
    {
        public SnakeCaseQueryValueProvider(BindingSource bindingSource, IQueryCollection values, CultureInfo culture) :
          base(bindingSource, values, culture)
        { }

        public override bool ContainsPrefix(string prefix)
        {
            return base.ContainsPrefix(prefix.ToSnakeCase());
        }

        public override ValueProviderResult GetValue(string key)
        {
            return base.GetValue(key.ToSnakeCase());
        }
    }
}
