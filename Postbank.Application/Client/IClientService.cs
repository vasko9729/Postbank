using Postbank.Application.Client.Responses;

namespace Postbank.Application.Client;

public interface IClientService
{
	Task<HomeConfigurationResponse> GetHomeConfiguration(CancellationToken cancellationToken);
}
