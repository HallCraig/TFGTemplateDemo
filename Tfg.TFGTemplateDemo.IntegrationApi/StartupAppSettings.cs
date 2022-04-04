// <copyright file="StartupAppSettings.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the StartupAppSettings class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using Tfg.Patterns;

    #endregion

    /// <summary>
    /// The startup application settings for the service
    /// For general configuration, this method should NOT be used, Instead IConfiguration should be injected into the 
    /// relevent class and the configuration accessed via that route.
    /// The below is expensive, and should only be used on startup before the DI container has been finalised. 
    /// </summary>
    public static class StartupAppSettings
    {
        public static string KafkaConsumerGroup => ConfigurationHelper.GetValue<string>("app.config:KafkaConsumerGroupId");

        public static string KafkaEnvironment => ConfigurationHelper.GetValue<string>("app.config:kafkaEnvironment");

        public static string SourceSystem => ConfigurationHelper.GetValue<string>("app.config:SourceSystem");
    }
}
