using Microsoft.AspNetCore.Builder;

namespace Postbank.Application;

public static class DependencyInjection
{
	public static WebApplicationBuilder AddApplication(this WebApplicationBuilder builder)
		=> builder;
}
