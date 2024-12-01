using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Postbank.Application.Client;
using Postbank.Application.Seeder;
using Postbank.Application.Users;
using Postbank.Infrastructure.Client;
using Postbank.Infrastructure.Configurations;
using Postbank.Infrastructure.Persistance;
using Postbank.Infrastructure.Seeder;
using Postbank.Infrastructure.Users;


namespace Postbank.Infrastructure;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
        => builder
            .AddConfigurations()
            .AddDbContext()
            .AddServices()
			.AddSeeder();

    private static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
    {
		builder.Services.Configure<ClientConfiguration>(builder.Configuration.GetSection(nameof(ClientConfiguration)));
		builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection(nameof(DatabaseConfiguration)));

        return builder;
    }

	public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
	{
		builder.Services.AddTransient<IClientService, ClientService>();
		builder.Services.AddTransient<IUserService, UserService>();

		return builder;
	}

	private static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(
            (sp, options) =>
            {
                var databaseConfiguration = sp.GetRequiredService<IOptions<DatabaseConfiguration>>();
                options.UseSqlServer(databaseConfiguration.Value.ConnectionString);
            });

        return builder;
    }

    private static WebApplicationBuilder AddSeeder(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISeeder, DatabaseSeeder>();

        return builder;
    }
}
