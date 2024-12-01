using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Postbank.Application.Users;
using Postbank.Application.Users.Requests;
using Postbank.Application.Users.Responses;
using Postbank.Infrastructure.Configurations;
using Postbank.Infrastructure.Persistance;

namespace Postbank.Infrastructure.Users;

internal class UserService : IUserService
{
	private readonly IOptions<ClientConfiguration> _clientConfiguration;
	private readonly ApplicationDbContext _applicationDbContext;

	public UserService(IOptions<ClientConfiguration> clientConfiguration, ApplicationDbContext applicationDbContext)
	{
		_clientConfiguration = clientConfiguration;
		_applicationDbContext = applicationDbContext;
	}

	public async Task<UsersResponse> GetUsers(UsersRequest request)
	{
		var pageOffset = request.CurrentPage - 1;

		var users = await _applicationDbContext.Users
			.Include(x => x.TimeLogs.Where(x => x.Date >= (request.From ?? DateTime.MinValue)))
			.ToListAsync();

		var orderedAndFilteredUsers = users
			.OrderByDescending(x => x.TimeLogs.Sum(x => x.Time.Value))
			.Skip(pageOffset * _clientConfiguration.Value.MaxPageUsers)
			.Take(_clientConfiguration.Value.MaxPageUsers)
			.Select(x => new UserResponse(x.Id, $"{x.FirstName} {x.LastName}", Math.Round(x.TimeLogs.Sum(x => x.Time.Value), 2)))
			.ToList();

		return new UsersResponse(orderedAndFilteredUsers);
	}
}
