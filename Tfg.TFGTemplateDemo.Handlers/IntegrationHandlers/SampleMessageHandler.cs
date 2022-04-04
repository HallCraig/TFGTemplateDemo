// <copyright file="SampleMessageHandler.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the SampleMessageHandler class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using System;
    using System.Text.Json;
    using Tfg.MessageBrokerEntity;

    #endregion

    public class SampleMessageHandler
    {
        #region Public

        /// <summary>
        /// Handles the subscription of the sale return entity from the message broker
        /// </summary>
        /// <param name="response">The message broker response</param>
        public void SubscribeSampleMessage(MessageBrokerResponse response)
        {
            // Make sure that only valid messages are processed and the end of the message queue has not been reached
            if(DetermineEndTopic(response))
            {
                return;
            }

            // Deserialize the message from JSON back into the entity.
            // The below uses System.Text.Json to deserialize (this should be the default),
            // but in some cases Newtonsoft.Json will need to be used 
            var message = JsonSerializer.Deserialize<SampleMessage>(response.Message.ToString());

            Console.WriteLine($"Message with 'Name' of '{message.Name}' successfully processed.");
        }

        #endregion

        #region Private

        /// <summary>
        /// Determines whether or not this is the end of the topic.
        /// </summary>
        /// <param name="busResponse">The bus response.</param>
        /// <param name="callerMemberName">The caller member name.</param>
        /// <returns>
        /// Returns whether or not this is the end of the topic.
        /// </returns>
        private bool DetermineEndTopic(MessageBrokerResponse busResponse)
        {
            var endTopic = false;

            //// WB: 2021-05-12: Need status specifically for "Reached end of topic" scenario and change this to check for that:
            ////                 BusResponse needs StatusCode so that Status (or rather StatusName) can change without impacting consumers:
            if (busResponse.Status.HasErrors || busResponse.Status.IsPartitionEndOfFile)
            {
                endTopic = true;
            }

            return endTopic;
        }

        #endregion
    }
}
