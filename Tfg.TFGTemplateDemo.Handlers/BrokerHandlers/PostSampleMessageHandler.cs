// <copyright file="PostSampleMessageHandler.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the PostSampleMessageHandler class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    using Microsoft.Extensions.Configuration;
    using System.Threading.Tasks;
    using Tfg.Patterns;

    /// <summary>
    /// Handler used to handle the POSTing of a sample message
    /// </summary>
    public class PostSampleMessageHandler : IRequestHandler<PostSampleMessageRequest, PostSampleMessageResponse>
    {
        /// <summary>
        ///  The message bus publisher
        /// </summary>
        private readonly IBusPublisher _publisher;

        /// <summary>
        /// The configuration interface
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructore
        /// </summary>
        /// <param name="publisher">The bus publisher</param>
        public PostSampleMessageHandler(IBusPublisher publisher, IConfiguration configuration)
        {
            _publisher = publisher;
            _configuration = configuration;
        }

        /// <summary>
        /// Handle the request 
        /// </summary>
        /// <param name="request">The request from the endpoint call</param>
        /// <returns>The response</returns>
        public async Task<PostSampleMessageResponse> HandleRequestAsync(PostSampleMessageRequest request)
        {
            // create the message containing the data
            var sampleMessage = new SampleMessage
            {
                Name = request.Name,
            };

            // create the message broker "envelope" 
            var brokerRequest = new MessageBrokerProducerRequest
            {
                Environment = _configuration.GetValue<string>("app.config:KafkaEnvironment"),
                SourceSystem = _configuration.GetValue<string>("app.config:SourceSystem"),
                Topic = "sample",
                Message = sampleMessage
            };
            
            await _publisher.PublishResponseAsync(brokerRequest);

            return new PostSampleMessageResponse
            {
                Message = "Message published successfully"
            };
        }
    }
}
