// <copyright file="ProductSchedulerApi.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the ProductSchedulerApi class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.HttpSys;
    using Microsoft.AspNetCore.Server.IISIntegration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using Tfg.EntityFramework;
    using Tfg.Patterns;

    #endregion

    /// <summary>
    /// The startup class
    /// </summary>
    public static class ProductSchedulerApi
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
                    webBuilder.ConfigureServices(services => services
                        .AddCors(options =>
                            {
                                options.AddDefaultPolicy(
                                builder =>
                                {
                                    builder.AllowAnyOrigin()
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
                        .AddControllers().Services
                        .AddAppBuilder()
                        .AddAuditor((b) => b
                            .AddLoggerAuditSink()
                            .AddPersistenceAuditSink<AuditTransaction>("tfg.tfgtemplatedemo")
                        )
                        .AddPersistence(builder => builder
                            .AddSql<DatabaseContext>("tfg.tfgtemplatedemo").AddEntityHelper()
                        )
                        .AddAppWorker((workerBuilder, serviceCollection, options) => workerBuilder.AddHangfire(serviceCollection, options),
                            jobBuilder => jobBuilder
                               .AddRecurringJob<ProductExtractHandler>("Product", x => x.ExtractProduct(), "0 * * * *")
                               .AddRecurringJob("MinuteClock", () => Console.WriteLine(DateTime.Now), "* * * * *")
                         )
                        .AddAppWorkerServer((serverBuilder, serviceCollection) => serverBuilder.AddHangfireServer(serviceCollection))
                        .Build()
                        )

                        .Configure(app => app
                            .UseApiDeveloperExceptionPage()
                            .UseHttpsRedirection()
                            .UseRouting()
                            .UseAuthentication()
                            .UseAuthorization()
                            .UseApiAuditor()
                            .UseCors()
                            .UseInitializers()
                            .UseRestErrorExceptionHandler()
                            .UseAppWorker()
                            .UseAppWorkerDashboard()
                        );
                }).UseStructuredLogging(builder => builder.EnableConsoleSink(true));
    }
}
