// <copyright file="ProductResponse.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the ProductResponse class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using System;

    #endregion

    public class ProductResponse
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the user created.
        /// </summary>
        /// <value>
        /// The user created.
        /// </value>
        public string UserCreated { get; set; }

        /// <summary>
        /// Gets or sets the user modified.
        /// </summary>
        /// <value>
        /// The user modified.
        /// </value>
        public string UserModified { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>
        /// The date modified.
        /// </value>
        public DateTimeOffset DateModified { get; set; }
    }
}
