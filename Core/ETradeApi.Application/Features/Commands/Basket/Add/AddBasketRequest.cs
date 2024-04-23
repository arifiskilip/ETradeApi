using MediatR;

namespace ETradeApi.Application.Features.Commands.Basket.Add
{
	public class AddBasketRequest : IRequest<AddBasketResponse>
	{
		public string ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
