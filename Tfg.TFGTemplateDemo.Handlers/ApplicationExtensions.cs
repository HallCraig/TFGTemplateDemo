// <copyright file="ApplicationExtensions.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the ApplicationExtensions class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    #endregion

    public static class ApplicationExtensions
    {
        public static IServiceCollection ForceAssemblyLoad(this IServiceCollection serviceCollection)
        {
            var refAssembyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            //Load referenced assemblies
            foreach (var asslembyNames in refAssembyNames)
            {
                Assembly.Load(asslembyNames);
            }

            return serviceCollection;
        }
    }
}
