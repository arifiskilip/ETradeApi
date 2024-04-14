using ETradeApi.Application.Abstractions.Hubs;
using ETradeApi.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ETradeApi.SignalR.HubServices
{
	public class ProductHubService : IProductHubService
	{
		private readonly IHubContext<ProductHub> _hubContext;

		public ProductHubService(IHubContext<ProductHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async Task ProductAddedMessageAsync(string message)
		{
			await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.ProductAddedMessage, message);
		}
	}
}
