// <copyright file="ProductExtractHandler.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the ProductExtractHandler class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using System.Threading.Tasks;

    #endregion

    public class ProductExtractHandler
    {
        /// <summary>
        /// An example method which is run on a schedule
        /// </summary>
        /// <returns>A task</returns>
        public Task ExtractProduct()
        {
            // Execute logic here to extract on a schedule

            return Task.CompletedTask;
        }
    }
}
