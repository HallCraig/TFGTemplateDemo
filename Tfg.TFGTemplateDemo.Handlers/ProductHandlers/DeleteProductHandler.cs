// <copyright file="DeleteProductHandler.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the DeleteProductHandler class.

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

    public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, DeleteProductResponse>
    {
        /// <summary>
        /// The entity helper
        /// </summary>
        private readonly IEntityHelper _entityHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductHandler"/> class.
        /// </summary>
        /// <param name="entityHelper">The entity helper.</param>
        public DeleteProductHandler(IEntityHelper entityHelper)
        {
            _entityHelper = entityHelper;
        }

        /// <summary>
        /// Handles the request asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<DeleteProductResponse> HandleRequestAsync(DeleteProductRequest request)
        {
            DeleteProductResponse responseProduct;
            Product entity = null;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, DeleteProductResponse>();
            });

            IMapper iMapper = config.CreateMapper();

            entity = await _entityHelper.GetEntityAsync<Product>(x => x.Id == request.Id);

            entity.UserModified = request.UserModified;
            entity.DateModified = DateTime.Now;
            entity.IsRetired = true;
            await _entityHelper.SaveEntityAsync(entity);

            responseProduct = iMapper.Map<Product, DeleteProductResponse>(entity);

            return await Task.FromResult(responseProduct);
        }
    }
}
