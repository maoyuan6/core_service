using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Options;

namespace Infrastructure.Extensions
{
    public static partial class Extensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            // Inject an implementation of ISwaggerProvider with defaulted settings applied.
            services.AddSwaggerGen();

            // Add the detail information for the API.
            services.ConfigureSwaggerGen(options =>
            {
                //// Define the OAuth2.0 scheme that's in use (i.e. Implicit Flow)
                //options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                //{
                //    Type = "oauth2",
                //    Flow = "implicit",
                //    AuthorizationUrl = "https://git.hetaozs.com/oauth/authorize",
                //    Scopes = new Dictionary<string, string>
                //    {
                //        { "readAccess", "Access read operations" },
                //        { "writeAccess", "Access write operations" }
                //    }
                //});

                //// Assign scope requirements to operations based on AuthorizeAttribute
                //options.OperationFilter<SecurityRequirementsOperationFilter>();

                options.SwaggerDoc("Basic", new OpenApiInfo
                {
                    Version = "Basic",
                    Title = "应用程序主要接口",
                    Description = "ASP.NET Core Web API Basic",
                });
                options.SwaggerDoc("Auth", new OpenApiInfo
                {
                    Version = "Auth",
                    Title = "权限接口",
                    Description = "ASP.NET Core Web API Auth",
                });
                options.SwaggerDoc("Commodity", new OpenApiInfo
                {
                    Version = "Commodity",
                    Title = "订单接口",
                    Description = "ASP.NET Core Web API Commodity",
                });
                options.SwaggerDoc("Gateway", new OpenApiInfo
                {
                    Version = "Gateway",
                    Title = "网关接口",
                    Description = "ASP.NET Core Web API Gateway",
                });
                options.SwaggerDoc("Probability", new OpenApiInfo
                {
                    Version = "Probability",
                    Title = "概率接口",
                    Description = "ASP.NET Core Web API Probability",
                });

                options.IncludeAllXmlComments();

                #region 枚举描述

                //options.UseInlineDefinitionsForEnums();
                options.AddEnumsWithValuesFixFilters(o =>
                {
                    // add schema filter to fix enums (add 'x-enumNames' for NSwag) in schema
                    o.ApplySchemaFilter = true;

                    // add parameter filter to fix enums (add 'x-enumNames' for NSwag) in schema parameters
                    o.ApplyParameterFilter = true;

                    // add document filter to fix enums displaying in swagger document
                    o.ApplyDocumentFilter = true;

                    // add descriptions from DescriptionAttribute or xml-comments to fix enums (add 'x-enumDescriptions' for schema extensions) for applied filters
                    o.IncludeDescriptions = true;

                    // add remarks for descriptions from xml-comments
                    o.IncludeXEnumRemarks = true;

                    // get descriptions from DescriptionAttribute then from xml-comments
                    o.DescriptionSource = DescriptionSources.DescriptionAttributesThenXmlComments;

                    // get descriptions from xml-file comments on the specified path
                    // should use "options.IncludeXmlComments(xmlFilePath);" before
                    //o.IncludeXmlCommentsFrom(xmlFilePath);
                    // the same for another xml-files...
                });

                #endregion

                options.DocInclusionPredicate((name, desc) =>
                {
                    var versions = desc.CustomAttributes()
                        .OfType<ApiExplorerSettingsAttribute>()
                        .Select(attr => attr.GroupName).ToList();
                    if (versions.Any(v => v == name))
                    {
                        var controllerActionDescriptor = desc.ActionDescriptor as ControllerActionDescriptor;
                        Console.WriteLine($"{desc.ActionDescriptor.DisplayName} - {controllerActionDescriptor.MethodInfo.ReturnType}");
                        return controllerActionDescriptor?.MethodInfo.ReturnType != typeof(Task);
                    }
                    return false;
                });

                // Use method name as operationId
                options.CustomOperationIds(apiDesc =>
                {
                    return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
                });

                //这里是给Swagger添加验证的部分
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            //services.AddSwaggerGenNewtonsoftSupport(); // explicit opt-in - needs to be placed after AddSwaggerGen()

            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "swagger/ui";
                options.SwaggerEndpoint("/swagger/Basic/swagger.json", "Basic Docs");
                options.SwaggerEndpoint("/swagger/Auth/swagger.json", "Auth Docs");
                options.SwaggerEndpoint("/swagger/Commodity/swagger.json", "Commodity Docs");
                options.SwaggerEndpoint("/swagger/Gateway/swagger.json", "Gateway Docs");
                options.SwaggerEndpoint("/swagger/Probability/swagger.json", "Probability Docs");
            });

            return app;
        }

        public static void IncludeAllXmlComments(this SwaggerGenOptions options)
        {
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var di = new DirectoryInfo(basePath);
            foreach (var fi in di.GetFiles("*.xml"))
            {
                options.IncludeXmlComments(fi.FullName);
            }
        }

        public static void IncludeAllXmlComments(this SwaggerGenOptions options, params string[] files)
        {
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            foreach (var fi in files)
            {
                options.IncludeXmlComments(Path.Combine(basePath, fi));
            }
        }
    }
}
