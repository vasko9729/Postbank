using Postbank.Web.Client.Requests;
using Postbank.Web.Client.Responses;

namespace Postbank.Web.Client;

public interface IDataClient
{
	Task<HomeConfigurationResponse> GetHomeConfiguration();

	Task<UsersResponse> GetUsers(UsersRequest request);

	Task SeedDatabase();
}
