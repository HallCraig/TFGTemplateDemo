// <copyright file="DeleteProductRequest.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the DeleteProductRequest class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using System;

    #endregion

    public class DeleteProductRequest
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user modified.
        /// </summary>
        /// <value>
        /// The user modified.
        /// </value>
        public string UserModified { get; set; }

    }
}
