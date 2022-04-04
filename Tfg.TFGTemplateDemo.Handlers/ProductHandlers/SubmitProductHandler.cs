// <copyright file="SubmitProductHandler.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the SubmitProductHandler class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Tfg.Patterns;

    #endregion

    public class SubmitProductHandler : IRequestHandler<SubmitProductRequest, ProductResponse>
    {
        /// <summary>
        /// The entity helper
        /// </summary>
        private readonly IEntityHelper _entityHelper;

        /// <summary>
        /// The http context accessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// The app errro factory
        /// </summary>
        private readonly IAppErrorFactory _errorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitProductHandler"/> class.
        /// </summary>
        /// <param name="entityHelper">The entity helper.</param>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        /// <param name="errorFactory">The error factory.</param>
        public SubmitProductHandler(IEntityHelper entityHelper, IHttpContextAccessor httpContextAccessor, IAppErrorFactory errorFactory)
        {
            _entityHelper = entityHelper;
            _httpContextAccessor = httpContextAccessor;
            _errorFactory = errorFactory;
        }

        /// <summary>
        /// Handles the request asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task<ProductResponse> HandleRequestAsync(SubmitProductRequest request)
        {
            ProductResponse response;
            Product entity = null;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductResponse>();
            });
            
            IMapper iMapper = config.CreateMapper();

            if (_httpContextAccessor.HttpContext.Request.Method == "POST")
            {
                if (request.Id == Guid.Empty)
                {
                    request.Id = Guid.NewGuid();
                }
                entity = new Product
                {                    
                    Id = request.Id,
                    Code = request.Code,
                    Name = request.Name,
                    DateCreated = DateTime.Now,
                    UserCreated = request.User,
                    UserModified = null,
                    IsRetired = false
                };
            }
            else
            {
                entity = await _entityHelper.GetEntityAsync<Product>(x => x.Id == request.Id && !x.IsRetired);

                if (entity != null)
                {
                    entity.Code = request.Code;
                    entity.Name = request.Name;
                    entity.UserModified = request.User;
                    entity.DateModified = DateTime.Now;
                }
                else
                {
                    var error = _errorFactory.GenerateError((int)HttpStatusCode.BadRequest, "BadArguments", "Data not found", "Update Product");
                    error.AddErrorDetail("Blank", "No record found for Id: " + request.Id.ToString());
                    error.ThrowException();
                }
            }
                        
            response = iMapper.Map<Product, ProductResponse>(entity);

            await _entityHelper.SaveEntityAsync(entity);
            
            return await Task.FromResult(response);
        }
    }
}
