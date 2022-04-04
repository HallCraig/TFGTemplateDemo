// <copyright file="Program.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the Program class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Server.HttpSys;
    using Microsoft.AspNetCore.Server.IISIntegration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;
    using Tfg.EntityFramework;
    using Tfg.Patterns;

    #endregion

    /// <summary>
    /// The initial startup class
    /// </summary>
    public static class ProductIntegrationApi
    {
        /// <summary>
        /// The Kafka consumer group identifier.
        /// </summary>
        private static readonly string kafkaConsumerGroupId = StartupAppSettings.KafkaConsumerGroup;

        /// <summary>
        /// The Kafka environment.
        /// </summary>
        private static readonly string kafkaEnvironment = StartupAppSettings.KafkaEnvironment;

        /// <summary>
        /// The PMM product attribute consumer request.
        /// </summary>
        private static readonly MessageBrokerConsumerRequest sampleMessageConsumerRequest = new MessageBrokerConsumerRequest
        {
            ConsumerGroupId = kafkaConsumerGroupId,
            Environment = kafkaEnvironment,
            SourceSystem = StartupAppSettings.SourceSystem,
            Topic = "sample"
        };

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
                        .AddAuditor((auditBuilder) => auditBuilder
                            .AddLoggerAuditSink()
                            .AddPersistenceAuditSink<AuditTransaction>("tfg.tfgtemplatedemo")
                         )
                        .AddPersistence(builder => builder
                            .AddSql<DatabaseContext>("tfg.tfgtemplatedemo").AddEntityHelper()
                        )
                        .AddConfluentKafkaHandlers()
                        .AddMessageBroker(brokerBuilder => brokerBuilder.AddConfluentKafka())
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
                            .UseEndpoints(endpointbuilder =>
                            {
                                endpointbuilder.MapGet("/", x => x.Response.WriteAsync("Product Integration Api"));
                                endpointbuilder.MapControllers();
                            })
                            .UseMessageBroker(usageBuilder => usageBuilder.UseConfluentKafka()
                                .Subscribe<MessageBrokerConsumerRequest>(sampleMessageConsumerRequest, async (serviceProvider, message, context) => { serviceProvider.GetRequiredService<IAuditor>().WrapCall("SubscribeSampleMessage", message, serviceProvider.GetRequiredService<SampleMessageHandler>().SubscribeSampleMessage); await Task.CompletedTask; })
                            )
                        );
                }).UseStructuredLogging(builder => builder.EnableConsoleSink(true));
    }
}
