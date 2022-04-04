// <copyright file="DatabaseContext.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the DatabaseContext class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using Microsoft.EntityFrameworkCore;

    #endregion

    public class DatabaseContext : DatabaseSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
        /// </summary>
        public DatabaseContext() : base()
        {

        }
    }
}
