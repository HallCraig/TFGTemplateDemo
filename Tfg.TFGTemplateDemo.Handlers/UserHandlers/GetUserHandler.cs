using ActiveDirectoryHelperService;
using AutoMapper;
using System.Threading.Tasks;
using Tfg.Patterns;

namespace Tfg.TFGTemplateDemo
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
    {
        private readonly IWcfClientFactory _factory;

        public GetUserHandler(IWcfClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<GetUserResponse> HandleRequestAsync(GetUserRequest request)
        {
            GetUserResponse contract;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ActiveDirectoryUser, GetUserResponse>();
            });

            IMapper iMapper = config.CreateMapper();

            var client = _factory.GetService<IActiveDirectoryHelperService>();
            ActiveDirectoryUser entity = await client.GetUserAsync(request.Username);
            contract = iMapper.Map<ActiveDirectoryUser, GetUserResponse>(entity);
            
            return await Task.FromResult(contract);
        }
    }
}
