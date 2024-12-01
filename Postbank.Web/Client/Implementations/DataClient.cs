using Microsoft.AspNetCore.Http;
using Postbank.Web.Client.Requests;
using Postbank.Web.Client.Responses;
using System.Net.Http;
using System.Net.Http.Json;

namespace Postbank.Web.Client.Implementations;

public class DataClient : IDataClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    public DataClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<HomeConfigurationResponse> GetHomeConfiguration()
    {
        var httpClient = _httpClientFactory.CreateClient(nameof(DataClient));

        var response = await httpClient.GetFromJsonAsync<HomeConfigurationResponse>("/api/home/configuration");

        return response!;
    }

    public async Task<UsersResponse> GetUsers(UsersRequest request)
    {
        var httpClient = _httpClientFactory.CreateClient(nameof(DataClient));

        var parameters = new Dictionary<string, string>();

		if (request.CurrentPage != 1)
        {
            parameters.Add("currentPage", request.CurrentPage.ToString());
		}

        if (request.From.HasValue)
        {
			parameters.Add("from", request.From.ToString()!);
		}

        var queryParameters= parameters
            .Select(x => $"{x.Key}={x.Value}")
            .ToList();

		var relativeUrl = $"api/users?{string.Join("&", queryParameters)}";

        var response = await httpClient.GetFromJsonAsync<UsersResponse>(relativeUrl);

        return response!;
	}

    public async Task SeedDatabase()
    {
        var httpClient = _httpClientFactory.CreateClient(nameof(DataClient));

        var _ = await httpClient.PostAsync("api/seeder/database", default);
    }
}
