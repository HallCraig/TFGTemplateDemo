// <copyright file="WcfCredential.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the WcfCredential class.

// Date created:    02 08 2022


namespace Tfg.TFGTemplateDemo
{
    #region Usings


    #endregion

    /// <summary>
    /// Defines the WcfCredential class.
    /// </summary>
    /// <remarks>
    /// Not applicable.
    /// </remarks>
    public class WcfCredential
    {
        /* Guideline: Use regions to group blocks of code as this makes the code more readable. The suggested
                      regions below follow the ordering rules enforced by StyleCop. Regions can be removed if
                      they are not used. */

        public string Domain { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
