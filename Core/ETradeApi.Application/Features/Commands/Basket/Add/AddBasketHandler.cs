
using ETradeApi.Application.Abstractions.Services;
using ETradeApi.Application.Repositories;
using MediatR;

namespace ETradeApi.Application.Features.Commands.Basket.Add
{
	public class AddBasketHandler : IRequestHandler<AddBasketRequest, AddBasketResponse>
	{
		private readonly IBasketService _basketService;

		public AddBasketHandler(IBasketService basketService)
		{
			_basketService = basketService;
		}

		public async Task<AddBasketResponse> Handle(AddBasketRequest request, CancellationToken cancellationToken)
		{
			var result = await _basketService.AddAsync(new()
			{
				ProductId = Guid.Parse(request.ProductId),
				Quantity = request.Quantity
			});

			return new()
			{
				Basket = result,
			};
		}
	}
}
