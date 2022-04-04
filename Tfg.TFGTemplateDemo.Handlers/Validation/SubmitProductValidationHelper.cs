// <copyright file="SubmitProductValidationHelper.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the SubmitProductValidationHelper class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using Microsoft.AspNetCore.Http;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Tfg.Patterns;

    #endregion

    public class SubmitProductValidationHelper : IRequestValidator<SubmitProductRequest, ProductResponse>
    {
        /// <summary>
        /// The http context accessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// The app errro factory
        /// </summary>
        private readonly IAppErrorFactory _errorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductValidationHelper"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        /// <param name="errorFactory">The error factory.</param>
        public SubmitProductValidationHelper(IHttpContextAccessor httpContextAccessor, IAppErrorFactory errorFactory)
        {
            _errorFactory = errorFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Pres the validation.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The context.</param>
        public async Task PreValidationAsync(SubmitProductRequest request)
        {
            var error = _errorFactory.GenerateError((int)HttpStatusCode.BadRequest, "BadArguments", "Errors with input parameters", "Update Product");

            if ((_httpContextAccessor.HttpContext.Request.Method == "PUT") && (request.Id == Guid.Empty))
            {
                error.AddErrorDetail("Blank", "Id cannot be blank or null");
            }
                
            if (string.IsNullOrEmpty(request.Code))
            {
                error.AddErrorDetail("Blank", "Code cannot be blank or null");
            }
            if (string.IsNullOrEmpty(request.Name))
            {
                error.AddErrorDetail("Blank", "Name cannot be blank or null");
            }
            if (string.IsNullOrEmpty(request.User))
            {
                error.AddErrorDetail("Blank", "User cannot be blank or null");
            }

            error.ThrowException();

            await Task.CompletedTask;
        }

        /// <summary>
        /// Posts the validation.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="model">The model.</param>
        public async Task PostValidationAsync(SubmitProductRequest request, ProductResponse model)
        {
            await Task.CompletedTask;
        }

    }
}
