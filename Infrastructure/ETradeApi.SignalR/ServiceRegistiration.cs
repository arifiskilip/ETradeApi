using ETradeApi.Application.Abstractions.Hubs;
using ETradeApi.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeApi.SignalR
{
	public static class ServiceRegistiration
	{
		public static void AddSignalRServices(this IServiceCollection services)
		{
			services.AddTransient<IProductHubService, ProductHubService>();
			services.AddSignalR();
		}
	}
}
