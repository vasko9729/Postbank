using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Postbank.Application.Client;
using Postbank.Application.Client.Responses;
using Postbank.Infrastructure.Configurations;
using Postbank.Infrastructure.Persistance;

namespace Postbank.Infrastructure.Client;

internal class ClientService : IClientService
{
    private readonly IOptions<ClientConfiguration> _clientConfigurations;
    private readonly ApplicationDbContext _applicationDbContext;

    public ClientService(IOptions<ClientConfiguration> clientConfiguration, ApplicationDbContext applicationDbContext)
    {
        _clientConfigurations = clientConfiguration;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<HomeConfigurationResponse> GetHomeConfiguration(CancellationToken cancellationToken)
	{
		var usersCount = await _applicationDbContext.Users.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling((double)(usersCount / _clientConfigurations.Value.MaxPageUsers));

        return new HomeConfigurationResponse(totalPages);
	}
}
