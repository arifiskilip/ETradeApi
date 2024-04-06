using ETradeApi.Application.Abstractions.Token;
using ETradeApi.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeApi.Infrastructure;

public static class ServiceRegistration
{
	public static void AddInfrastructureServices(this IServiceCollection services)
	{
		services.AddScoped<ITokenService, TokenManager>();
	}
}
