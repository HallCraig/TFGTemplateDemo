// <copyright file="SchedulerApiExtensions.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the SchedulerApiExtensions type.

// Date created:    23 Mar 2022

using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("TFG.Modules.Scheduler.SchedulerApiExtensions", Version = "1.0")]

namespace Tfg.TFGTemplateDemo
{
    using System;
    using Tfg.Patterns;

    public static class SchedulerApiExtensions
    {
        /// <summary>
        /// This methods is used to add any custom dependency injection configuration
        /// Access to IServiceCollection can be done through builder.Services
        /// </summary>
        /// <param name="builder">the IAppBuilder</param>
        /// <returns>The IAppBuilder</returns>
        [IntentManaged(Mode.Ignore)]
        public static IAppBuilder AddApiCustomisation(this IAppBuilder builder)
        {
            // add any application specific DI configuration here
            return builder;
        }

        /// <summary>
        /// Configure the Persistence Helper plus any specific database connections
        /// </summary>
        /// <param name="builder">The AppBuilder</param>
        /// <returns>The AppBuilder</returns>
        [IntentManaged(Mode.Ignore)]
        public static IAppBuilder AddApiPersistence(this IAppBuilder builder)
        {
            return builder.AddPersistence(builder => builder
                .AddSql<DatabaseContext>("tfg.tfgtemplatedemo").AddEntityHelper()
            );
        }

        /// <summary>
        /// Configure the IAuditor plus any sinks
        /// </summary>
        /// <param name="builder">The AppBuilder</param>
        /// <returns>The AppBuilder</returns>
        [IntentManaged(Mode.Ignore)]
        public static IAppBuilder AddApiAuditor(this IAppBuilder builder)
        {
            return builder.AddAuditor(auditBuilder => auditBuilder
                .AddLoggerAuditSink()
                .AddPersistenceAuditSink<AuditTransaction>("tfg.tfgtemplatedemo")
            );
        }

        /// <summary>
        /// Configure any jobs to be executed by the worker process
        /// </summary>
        /// <param name="builder">The AppBuilder</param>
        /// <returns>The AppBuilder</returns>
        [IntentManaged(Mode.Ignore)]
        public static IWorkerJobBuilder ConfigureWorkerJobs(this IWorkerJobBuilder builder)
        {
            return builder
                .AddRecurringJob<SampleSchedulerHandler>("ScheduledTask", x => x.Handle(), "0 * * * *")
                .AddRecurringJob("DailyClock", () => Console.WriteLine(DateTime.Now), "* 2 * * *");
        }


    }
}