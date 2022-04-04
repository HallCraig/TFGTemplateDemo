// <copyright file="Program.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the Program class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{

    #region Usings

    using ActiveDirectoryHelperService;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Server.HttpSys;
    using Microsoft.AspNetCore.Server.IISIntegration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Collections.Generic;
    using System.Net;
    using Tfg.EntityFramework;
    using Tfg.Patterns;

    #endregion

    /// <summary>
    /// The startup class
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
#if (LOCAL || DEBUG)
                .AddMigrations<DatabaseContext>()
#endif

                .Run();
        }

        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The host builder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureServices(services => services
                            .AddCors(options =>
                            {
                                options.AddDefaultPolicy(
                                builder =>
                                {
                                    builder.AllowAnyOrigin()
                                    .SetIsOriginAllowed(origin => true)
                                    .WithOrigins("*")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                                });
                            })
                            .AddAuthentication(IISDefaults.AuthenticationScheme).Services
                            .AddAuthentication(HttpSysDefaults.AuthenticationScheme).Services
                            .Configure<IISOptions>(options =>
                            {
                                options.ForwardClientCertificate = false;
                                options.AutomaticAuthentication = true;
                            })
                            .AddControllers().Services.ForceAssemblyLoad()
                            .AddAppBuilder()
                            .AddTokenEndpoint()
                            .AddJwtPolicies()
                            .AddJwtAuthentication()
                            .AddAuditor(auditBuilder => auditBuilder
                                .AddLoggerAuditSink()
                                .AddPersistenceAuditSink<AuditTransaction>("tfg.tfgtemplatedemo")
                            )
                            .AddPersistence(builder => builder
                                .AddSql<DatabaseContext>("tfg.tfgtemplatedemo").AddEntityHelper()
                             )
                            .AddRestApi()
                            .AddRestApiSwaggerDocs()
                            .AddWcf(wcfBuilder => wcfBuilder
                                .AddClient<IActiveDirectoryHelperService>("ActiveDirectoryService", 
                                    modifier => modifier
                                        .AddModifier(HttpBindings.SetHttpBindings) 
                                        .AddModifier(NtlmImpersonation.SetNtlmImpersonation)
                                        .AddModifier((client, endpoint, options, configuration) =>
                                        {
                                            var credentials = configuration.GetOptions<WcfCredential>("wcfCredentials");

                                            client.ClientCredentials.Windows.ClientCredential = new NetworkCredential
                                            {
                                                Domain = credentials.Domain,
                                                UserName = credentials.Username,
                                                Password = credentials.Password
                                            };
                                        }))
                             )
                            .AddRequestRouting()
                            .AddMessageBroker(b => b.AddConfluentKafka())
                            .Build()
                        )

                        .Configure(app => app
                            .UseApiDeveloperExceptionPage()
                            .UseHttpsRedirection()
                            .UseRouting()
                            .UseAuthentication()
                            .UseAuthorization()
                            .UseApiAuditor()
                            .UseSwaggerDocs()
                            .UseCors()
                            .UseInitializers()
                            .UseRestErrorExceptionHandler()
                            .UseTokenEndpoint()
                            .UseRestEndpoints(restendpointBuilder => restendpointBuilder
                                .Post<SubmitProductRequest, ProductResponse>("api/product", additionalInfo: new RestEndpointInfoDefinition { GroupName = "Product" })
                                .Put<SubmitProductRequest, ProductResponse>("api/product", additionalInfo: new RestEndpointInfoDefinition { GroupName = "Product" })
                                .Delete<DeleteProductRequest, DeleteProductResponse>("api/product/{Id}/{UserModified}", additionalInfo: new RestEndpointInfoDefinition { GroupName = "Product" })
                                .Get<GetProductRequest, ProductResponse>("api/product/code/{Code}", additionalInfo: new RestEndpointInfoDefinition { GroupName = "Product" })
                                .Get<GetProductRequest, ProductResponse>("api/product/id/{Id}", additionalInfo: new RestEndpointInfoDefinition { GroupName = "Product" })
                                .Get<GetProductsRequest, List<ProductResponse>>("api/product/name/{Name}", additionalInfo: new RestEndpointInfoDefinition { GroupName = "Product" })
                                .Get<GetProductsRequest, List<ProductResponse>>("api/products", additionalInfo: new RestEndpointInfoDefinition { GroupName = "Product" })
                                .Get<GetUserRequest, GetUserResponse>("api/user/{Username}", additionalInfo: new RestEndpointInfoDefinition { GroupName = "User" })
                                .Post<PostSampleMessageRequest, PostSampleMessageResponse>("api/brokersample", additionalInfo: new RestEndpointInfoDefinition { GroupName = "Broker Sample" })
                            )
                            .UseEndpoints(ep =>
                            {
                                ep.MapGet("ping", x => x.Response.WriteAsync("pong"));
                                ep.MapGet("error", x => throw new System.Exception("An manual error occured and was thrown"));
                                ep.MapControllers();
                            })
                    );
                }).UseStructuredLogging(builder => builder.EnableConsoleSink(true));
    }
}
