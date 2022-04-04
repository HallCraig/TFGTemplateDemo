// <copyright file="AuditTransaction.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the AuditTransaction class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using System;
    using Tfg.Patterns;

    #endregion

    public class AuditTransaction : IAuditTransaction
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

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

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public string UniqueId { get; set; }

        /// <summary>
        /// Gets or sets the time elapsed.
        /// </summary>
        /// <value>
        /// The time elapsed.
        /// </value>
        public long TimeElapsed { get; set; }

        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        public string ServiceName { get; set; }

        /// <summary>
        /// Gets or sets the name of the operation.
        /// </summary>
        /// <value>
        /// The name of the operation.
        /// </value>
        public string OperationName { get; set; }

        /// <summary>
        /// Gets or sets the type of the operation.
        /// </summary>
        /// <value>
        /// The type of the operation.
        /// </value>
        public string OperationType { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>
        /// The status identifier.
        /// </value>
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the type of the ledger.
        /// </summary>
        /// <value>
        /// The type of the ledger.
        /// </value>
        public string LedgerType { get; set; }

        /// <summary>
        /// Gets or sets the detail.
        /// </summary>
        /// <value>
        /// The detail.
        /// </value>
        public string Detail { get; set; }

        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        /// <value>
        /// The name of the server.
        /// </value>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>
        /// The ip address.
        /// </value>
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is retired.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is retired; otherwise, <c>false</c>.
        /// </value>
        public bool IsRetired { get; set; }

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
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public byte[] Version { get; set; }
    }
}
