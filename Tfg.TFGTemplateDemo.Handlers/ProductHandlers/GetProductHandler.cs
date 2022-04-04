// <copyright file="GetProductHandler.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the GetProductHandler class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using AutoMapper;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Tfg.Patterns;

    #endregion

    public class GetProductHandler : IRequestHandler<GetProductRequest, ProductResponse>
    {
        /// <summary>
        /// The entity helper
        /// </summary>
        private readonly IEntityHelper _entityHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductHandler"/> class.
        /// </summary>
        /// <param name="entityHelper">The entity helper.</param>
        public GetProductHandler(IEntityHelper entityHelper)
        {
            _entityHelper = entityHelper;
        }

        /// <summary>
        /// Handles the request asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<ProductResponse> HandleRequestAsync(GetProductRequest request)
        {
            ProductResponse model;
            Product entity = null;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductResponse>();
            });

            IMapper iMapper = config.CreateMapper();

            if (request.Id != Guid.Empty)
            {
                entity = await _entityHelper.GetEntityAsync<Product>(x => x.Id == request.Id && !x.IsRetired);
            }
            if (!string.IsNullOrEmpty(request.Code))
            {
                entity = await _entityHelper.GetEntityAsync<Product>(x => x.Code == request.Code && !x.IsRetired);
            }

            model = iMapper.Map<Product, ProductResponse>(entity);

            return await Task.FromResult(model);
        }
    }
}
