// <copyright file="SampleMessage.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the SampleMessage class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    /// <summary>
    /// Sample message entity used to publish/consume from the message broker
    /// </summary>
    public class SampleMessage
    {
        /// <summary>
        ///  A sample name
        /// </summary>
        public string Name { get; set; }
    }
}
