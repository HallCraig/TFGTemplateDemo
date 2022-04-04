// <copyright file="GetProductsHandler.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the GetProductsHandler class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using AutoMapper;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Tfg.Patterns;

    #endregion

    public class GetProductsHandler : IRequestHandler<GetProductsRequest, List<ProductResponse>>
    {
        /// <summary>
        /// The entity helper
        /// </summary>
        private readonly IEntityHelper _entityHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductsHandler"/> class.
        /// </summary>
        /// <param name="entityHelper">The entity helper.</param>
        public GetProductsHandler(IEntityHelper entityHelper)
        {
            _entityHelper = entityHelper;
        }

        /// <summary>
        /// Handles the request asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<ProductResponse>> HandleRequestAsync(GetProductsRequest request)
        {
            List<ProductResponse> model;
            List<Product> entities = null;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductResponse>();
            });

            IMapper iMapper = config.CreateMapper();

            if (!string.IsNullOrEmpty(request.Name))
            {
                entities = (await _entityHelper.GetEntitiesAsync<Product>(x => x.Name.Contains(request.Name))).ToList();
            }
            else
            {
                entities = (await _entityHelper.GetEntitiesAsync<Product>(x => !x.IsRetired)).ToList();
            }

            model = iMapper.Map<List<Product>, List<ProductResponse>>(entities);

            return await Task.FromResult(model);
        }
    }
}
