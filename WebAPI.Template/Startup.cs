using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.PlatformAbstractions;
using NAutowired;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Snowflake.Core;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.IO;
using WebAPI.Template.Filter;

namespace WebAPI.Template
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //加上自定有路由配置(加上此配置可使用自定义路由)
            services.AddRouting();
            //注册IOptions
            services.AddOptions();
            //配置文件
            ConfigureConfigurations(services);
            //统一配置时间类型输出加上本地时区
            services.AddMvc(options =>
            {
                options.ValueProviderFactories.Add(new SnakeCaseQueryValueProviderFactory());
            })
              .SetCompatibilityVersion(CompatibilityVersion.Latest)
              .AddJsonOptions(options =>
              {
                  options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                  options.SerializerSettings.Converters.Add(new LongToStringConverter());
                  options.SerializerSettings.Converters.Add(new EnumToStringConverter());
                  options.SerializerSettings.Converters.Add(new StringToNumericalValueConverter());
                  options.SerializerSettings.ContractResolver =
              new DefaultContractResolver() { NamingStrategy = new SnakeCaseNamingStrategy() };
                  options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
              });
            //添加SwaggerUI
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<AuthorizationFilter>();
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info { Title = "WebAPI Template API", Version = "v1" });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var webAppXmlPath = Path.Combine(basePath, "WebAPI.Template.xml");
                var bllXmlPath = Path.Combine(basePath, "Template.xml");
                var dalXmlPath = Path.Combine(basePath, "Template.xml");
                var extensionsXmlPath = Path.Combine(basePath, "Extensions.Template.xml");
                options.IncludeXmlComments(webAppXmlPath);
                options.IncludeXmlComments(bllXmlPath);
                options.IncludeXmlComments(dalXmlPath);
                options.IncludeXmlComments(extensionsXmlPath);
            });
            services.TryAddEnumerable(ServiceDescriptor
              .Transient<IApiDescriptionProvider, SnakeCaseQueryParametersApiDescriptionProvider>());
        }

        /// <summary>
        /// 配置文件读取
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureConfigurations(IServiceCollection services)
        {
            //数据库连接配置读取
            services.Configure<DAL.Template.Config>(Configuration.GetSection("DatabaseSettings"));
            services.AddSingleton(item => new IdWorker(1, Configuration.GetValue<int>("ServerNodeNo")));
            services.AddSingleton<IControllerActivator, NAutowiredControllerActivator>();
            //use auto dependency injection
            services.AddAutoDependencyInjection(new List<string> { "WebAPI.Template", "BLL.Template", "DAL.Template" });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler("/error/catch_all");
            app.UseDefaultFiles();
            app.UseStaticFiles();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI Template v1");
            });
            app.UseMvc();
        }
    }
}
