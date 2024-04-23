using MediatR;

namespace ETradeApi.Application.Features.Commands.Basket.Remove
{
	public class RemoveBasketRequest : IRequest<RemoveBasketResponse>
	{
		public string ProductId { get; set; }
		public int? Quantity { get; set; } 
	}
}
