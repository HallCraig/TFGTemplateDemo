// <copyright file="EndpointEnrichment.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the EndpointEnrichment class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using System.Net.Http;
    using Tfg.Patterns;

    #endregion

    /// <summary>
    /// Specifies the endpoint descriptions for Swagger
    /// </summary>
    /// <seealso cref="Tfg.Api.IEndpointEnrichment" />
    public class EndpointEnrichment : IEndpointEnrichment
    {
        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <value>
        /// The groups.
        /// </value>
        public IEnrichmentGroup[] Groups => new IEnrichmentGroup[]
        {
            new EnrichmentGroup("Product", "Basic CRUD Operations for Product"),
        };

        /// <summary>
        /// Gets the endpoints.
        /// </summary>
        /// <value>
        /// The endpoints.
        /// </value>
        public IEnrichmentEndpoint[] Endpoints => new IEnrichmentEndpoint[]
        {
            new EnrichmentEndpoint("api/product", HttpMethod.Post.ToString(), "Product", "Submit Product", "Create a new product", false)
                .AddExternalReference(""),

            new EnrichmentEndpoint("api/product", HttpMethod.Put.ToString(), "Product", "Submit Product", "Update an existing product information", false)
                .AddExternalReference(""),

            new EnrichmentEndpoint("api/product/id/{Id}", HttpMethod.Get.ToString(), "Product", "Get Product By Id", "Retrieve product information by using the product Id", false)
                .AddExternalReference("")
                .AddParameter("Id", "Product Id"),

            new EnrichmentEndpoint("api/product/code/{Code}", HttpMethod.Get.ToString(), "Product", "Get Product By Code", "Retrieve product information by using the product code", false)
                .AddExternalReference("")
                .AddParameter("Code", "Product Code"),

            new EnrichmentEndpoint("api/product/name/{Name}", HttpMethod.Get.ToString(), "Product", "Get Products By Name", "Retrieve products by using the product name", false)
                .AddExternalReference("")
                .AddParameter("Name", "Product Name"),

            new EnrichmentEndpoint("api/products", HttpMethod.Get.ToString(), "Product", "Get Products", "Retrieve all active products information", false)
                .AddExternalReference(""),

            new EnrichmentEndpoint("api/product/{Id}/{UserModified}", HttpMethod.Delete.ToString(), "Product", "Delete Product", "Delete product using the product Id", false)
                .AddExternalReference("")
                .AddParameter("Id", "Product Id")
                .AddParameter("UserModified", "User deleting record")
        };
    }
}
