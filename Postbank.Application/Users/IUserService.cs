using Postbank.Application.Users.Requests;
using Postbank.Application.Users.Responses;

namespace Postbank.Application.Users;

public interface IUserService
{
	Task<UsersResponse> GetUsers(UsersRequest request);
}
