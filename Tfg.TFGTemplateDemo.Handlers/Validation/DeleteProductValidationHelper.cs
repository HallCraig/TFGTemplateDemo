// <copyright file="DeleteProductValidationHelper.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the DeleteProductValidationHelper class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Tfg.Patterns;

    #endregion

    public class DeleteProductValidationHelper : IRequestValidator<DeleteProductRequest, DeleteProductResponse>
    {
        /// <summary>
        /// The app errro factory
        /// </summary>
        private readonly IAppErrorFactory _errorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductValidationHelper"/> class.
        /// </summary>
        /// <param name="errorFactory">The error factory.</param>
        public DeleteProductValidationHelper(IAppErrorFactory errorFactory)
        {
            _errorFactory = errorFactory;
        }

        /// <summary>
        /// Pres the validation.
        /// </summary>
        /// <param name="request">The request.</param>
        public async Task PreValidationAsync(DeleteProductRequest request)
        {
            var error = _errorFactory.GenerateError((int)HttpStatusCode.BadRequest, "BadArguments", "Errors with input parameters", "Delete Product");
            if (request.Id == Guid.Empty)
            {
                error.AddErrorDetail("Blank", "Id cannot be blank or null");
            }
            if (string.IsNullOrEmpty(request.UserModified))
            {
                error.AddErrorDetail("Blank", "Modfied User may not be blank or null");
            }

            error.ThrowException();

            await Task.CompletedTask;
        }

        /// <summary>
        /// Posts the validation.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="model">The model.</param>
        public async Task PostValidationAsync(DeleteProductRequest request, DeleteProductResponse model)
        {
            await Task.CompletedTask;
        }

    }
}
