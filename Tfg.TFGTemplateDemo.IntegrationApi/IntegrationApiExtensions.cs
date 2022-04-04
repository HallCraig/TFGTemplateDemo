// <copyright file="IntegrationApiExtensions.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the IntegrationApiExtensions class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using Microsoft.Extensions.DependencyInjection;
    using Tfg.Patterns;

    #endregion

    public static class IntegrationApiExtensions
    {
        /// <summary>
        /// Adds the Confluent Kafka handlers.
        /// </summary>
        /// <param name="appBuilder">The app  builder.</param>
        /// <returns>
        /// Returns the app builder.
        /// </returns>
        public static IAppBuilder AddConfluentKafkaHandlers(this IAppBuilder appBuilder)
        {
            /*
             * Here you would add the handlers responsible for processing messages as they are consumed from Kafka.
             * Eg. If you had a ProductHandler to consume messages you would do the below.
            */

            appBuilder.Services.AddTransient(typeof(SampleMessageHandler));

            return appBuilder;
        }
    }
}
